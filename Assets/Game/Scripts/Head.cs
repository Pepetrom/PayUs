using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Head : MonoBehaviour
{
    //HotBar
    [SerializeField] int hotbarSize, actualIten;
    [SerializeField] GameObject[] itensInHotbar;
    //Misc
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] GameObject _head;
    private Camera _mainCamera;
    //Temporario
    public GameObject areaOfEffectAtack;
    private Material _effectVisual;
    //Movimento
    public bool canMove = true;
    //Mining
    [SerializeField] Transform _miningTrasformPoint;
    [SerializeField] Ore _atackTarget;
    [SerializeField] float _atackDelay = 1;
    public float atackRange;
    private bool _isMining;
    //Holding
    public PickableOre oreYouAreHolding;
    private bool _holding, _justPickedTheItem;
    public float throwingPower;
    //RayCast
    private RaycastHit _hit;
    private Ray _ray;
    [SerializeField] LayerMask _layerForRaycast;
    //Stamina Food
    private float _stamina = 1, _food = 1, _regenCooldown = 0;
    [SerializeField] private float _staminaPerHit,_staminaPerCraft, _foodSpentPerTick, _staminaGainedPerTick;

    void Start()
    {
        _mainCamera = Camera.main;
        GameManager.Instance.playerHead = this;
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
                GameManager.Instance.uiManager.UpdateHungerStamina(_food, _stamina);
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
                if (_justPickedTheItem)
                {
                    _justPickedTheItem = false;
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
                    _justPickedTheItem = true;
                }
                else
                {
                    _justPickedTheItem = false;
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
        oreYouAreHolding.Launch(throwingPower);
        oreYouAreHolding = null;
        _holding = false;
    }
    public void PlaceInStation()
    {
        oreYouAreHolding.Drop();
        oreYouAreHolding = null;
        _holding = false;
        _justPickedTheItem = false;
    }
    private void DropObject()
    {
        if (oreYouAreHolding != null)
        {
            oreYouAreHolding.Drop();
            oreYouAreHolding = null;
            _holding = false;
        }
    }
    private void PickUpObject()
    {
        if (Raycast())
        {
            if (_hit.collider.CompareTag("Pickable"))
            {
                oreYouAreHolding = _hit.collider.GetComponent<PickableOre>();
                oreYouAreHolding.PickUp(transform, _miningTrasformPoint);
                _holding = true;
            }
        }
    }
    public void MoveSelectionOnHotbar()
    {
        if (Input.GetAxis("MouseScrollWheel") == 1)
        {
            actualIten++;
            if (actualIten >= hotbarSize)
            {
                actualIten = 0;
            }
            GameManager.Instance.uiManager.UpdateHotbar(actualIten, 0);
        }
        else
        {
            actualIten--;
            if (actualIten < 0)
            {
                actualIten = hotbarSize - 1;
            }
            GameManager.Instance.uiManager.UpdateHotbar(actualIten, 0);
        }
        Debug.Log(Input.GetAxis("MouseScrollWheel"));
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
        GameManager.Instance.uiManager.UpdateHungerStamina(_food, _stamina);
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
