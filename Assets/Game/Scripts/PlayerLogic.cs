using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLogic : MonoBehaviour
{
    //Misc
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private GameObject _head;
    private Camera _mainCamera;
    //Temporario
    public GameObject areaOfEffectAtack;
    private Material _effectVisual;
    //Movimento
    public bool canMove = true;
    //Mining
    [SerializeField] private Transform _miningTrasformPoint;
    [SerializeField] private Ore _atackTarget;
    [SerializeField] private float _atackDelay = 1;
    public float atackRange;
    private bool _isMining;
    //HotBar
    [SerializeField] private int _hotbarSize, _actualIten;
    [SerializeField] private PickableIten[] itensInHotbar;
    //Holding
    public PickableIten itenYouAreHolding;
    private bool _holding, _justPickedTheIten;
    public float throwingPower;
    //RayCast
    private RaycastHit _hit;
    private Ray _ray;
    [SerializeField] private LayerMask _layerForRaycast;
    //Stamina Food
    private float _stamina = 1, _food = 1, _regenCooldown = 0;
    [SerializeField] private float _staminaPerHit,_staminaPerCraft, _foodSpentPerTick, _staminaGainedPerTick;

    void Start()
    {
        _mainCamera = Camera.main;
        GameManager.instance.playerLogic = this;
        itensInHotbar = new PickableIten[_hotbarSize];
        //Temporario
        _effectVisual = areaOfEffectAtack.GetComponent<Renderer>().material;
    }
    private void Update()
    {
        if (canMove)
        {
        InputCheck();
        }
        RegenStamina();
    }
    void FixedUpdate()
    {
        Aim();
    }
    public void Craft()
    {      
        _stamina -= _staminaPerCraft;
        canMove = true;
    }
    public bool Raycast()
    {
        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_ray, out _hit);
        if (Vector3.Distance(transform.position, _hit.collider.transform.position)< atackRange)
        {
            return true;
        }
        return false;
        
    }
    private void RegenStamina()
    {
        if (_regenCooldown < 1)
        {
            _regenCooldown += Time.deltaTime;
        }
        else
        {
            if (_stamina < 1 && _food > 0)
            {
                _stamina += _staminaGainedPerTick;
                _food -= _foodSpentPerTick;
                _regenCooldown = 0;
                GameManager.instance.uiManager.UpdateHungerStamina(_food, _stamina);
            }
        }
    }
    private void InputCheck()
    {
        if (!_isMining)
        {
            //MouseScrollWheel
            if (Input.GetAxis("MouseScrollWheel") != 0)
            {
                MoveSelectionOnHotbar();
            }
            //MouseRightButton
            if (Input.GetMouseButton(0) && !_holding)
            {
                StartCoroutine(DealDamage());
            }
            //MouseLeftButton
            if (Input.GetMouseButtonUp(1))
            {
                if (_justPickedTheIten)
                {
                    _justPickedTheIten = false;
                }
                else
                {
                    if (throwingPower < 0.5f)
                    {
                        DropObject();
                    }
                    else
                    {
                        LaunchObject();
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (!_holding)
                {
                    PickUpObject();
                    _justPickedTheIten = true;
                }
                else
                {
                    _justPickedTheIten = false;
                }
            }
            if (Input.GetMouseButton(1))
            {
                if (throwingPower < 1)
                {
                    throwingPower += Time.deltaTime;
                }
            }
            else
            {
                throwingPower = 0;
            }
        }
    }
    private void LaunchObject()
    {
        itenYouAreHolding.Launch(throwingPower);
        itenYouAreHolding = null;
        _holding = false;
    }
    public void PlaceInStation()
    {
        itenYouAreHolding.Drop();
        itenYouAreHolding = null;
        _holding = false;
        _justPickedTheIten = false;
    }
    private void DropObject()
    {
        if (itenYouAreHolding != null)
        {
            GameManager.instance.uiManager.UpdateSingleItenInHotbar(_actualIten, 0);
            itenYouAreHolding.Drop();
            if (Raycast())
            {
                if (_hit.collider.gameObject.layer == 8)
                {
                    _hit.collider.GetComponent<Storage>().TryStore(itenYouAreHolding);
                }
            }
            itenYouAreHolding = null;
            itensInHotbar[_actualIten] = null;
            _holding = false;
        }      
    }
    private void PickUpObject()
    {
        if (Raycast())
        {
            if (_hit.collider.CompareTag("Pickable"))
            {
                if (CheckHotbarSpace(_actualIten))
                {
                    itensInHotbar[_actualIten] = _hit.collider.GetComponent<PickableIten>();
                    itenYouAreHolding = itensInHotbar[_actualIten];
                    itenYouAreHolding.PickUp(transform, _miningTrasformPoint);
                    _holding = true;
                    GameManager.instance.uiManager.UpdateSingleItenInHotbar(_actualIten, itenYouAreHolding.Id+1);
                }
            }
            //This part is here and not in drop because the drop occurs after this, so if you click with a iten in hand in a storage, it will store before trying to drop
            if (_hit.collider.gameObject.layer == 8)
            {
                _hit.collider.GetComponent<Storage>().TryStore(itenYouAreHolding);
            }
        }
    }
    private bool CheckHotbarSpace(int firstToTest)
    {
        if (itensInHotbar[firstToTest] == null)
        {
            _actualIten = firstToTest;

            return true;
        }
        for (int i = 0; i < itensInHotbar.Length; i++)
        {
            if (itensInHotbar[i] == null)
            {
                _actualIten = i;

                return true;
            }
        }
        return false;
    }
    public void MoveSelectionOnHotbar()
    {
        if (Input.GetAxis("MouseScrollWheel") == -1)
        {
            if(itensInHotbar[_actualIten] != null)
            {
            itensInHotbar[_actualIten].gameObject.SetActive(false);
            }
            _actualIten++;
            if (_actualIten >= _hotbarSize)
            {
                _actualIten = 0;
            }
            if (itensInHotbar[_actualIten] != null)
            {
                itensInHotbar[_actualIten].gameObject.SetActive(true);
            }
            itenYouAreHolding = itensInHotbar[_actualIten];
            GameManager.instance.uiManager.UpdateHotbar(_actualIten);
        }
        else
        {
            if (itensInHotbar[_actualIten] != null)
            {
                itensInHotbar[_actualIten].gameObject.SetActive(false);
            }
            _actualIten--;
            if (_actualIten < 0)
            {
                _actualIten = _hotbarSize - 1;
            }
            if (itensInHotbar[_actualIten] != null)
            {
                itensInHotbar[_actualIten].gameObject.SetActive(true);
            }
            itenYouAreHolding = itensInHotbar[_actualIten];
            GameManager.instance.uiManager.UpdateHotbar(_actualIten);
        }
        if (itensInHotbar[_actualIten] == null)
        {
            _holding = false;
        }
        else
        {
            _holding = true;
        }
    }
    

    private IEnumerator DealDamage()
    {
        //cubo muda de cor para indicar q está funcionando, remover com adição de animação
        _effectVisual.color = Color.green;
        //---
        _isMining = true;
        if (Raycast())
        {
            if (_hit.collider.CompareTag("Ore"))
            {
                _atackTarget = _hit.collider.GetComponent<Ore>();
                _atackTarget.takeDamage(1);
            }
        }
        //animator.SetTrigger("Swing");
        yield return new WaitForSeconds(_atackDelay);
        //cubo muda de cor para indicar q está funcionando, remover com adição de animação
        _effectVisual.color = Color.grey;
        //---
        _stamina -= _staminaPerHit;
        GameManager.instance.uiManager.UpdateHungerStamina(_food, _stamina);
        _isMining = false;
    }
    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;
            // Ignore the height difference.
            direction.y = 0;
            // Make the transform look in the direction.
            transform.forward = direction;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, 100, _groundMask))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }

}
