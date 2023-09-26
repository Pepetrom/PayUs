using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLogic : MonoBehaviour
{
    [Header("RayCast")]//RayCast
    private RaycastHit _hit;
    private Ray _ray;
    [SerializeField] private LayerMask _layerForRaycast;
    [SerializeField] private LayerMask _groundMask;
    private Camera _mainCamera;
    [Header("Temporary")]//Temporario
    public GameObject areaOfEffectAtack;
    private Material _effectVisual;
    [Header("Movement")]//Movimento
    public bool canMove = true;
    [Header("Mining")]//Mining
    [SerializeField] private Transform _miningTrasformPoint;
    [SerializeField] private Ore _atackTarget;
    [SerializeField] private float _atackDelay = 1;
    public float atackRange;
    private bool _isMining;
    //Stamina
    private float _stamina = 1;
    [SerializeField] private float _staminaPerHit, _staminaPerCraft;
    [Header("HotBar")]//HotBar
    [SerializeField] private int _hotbarSize;
    [SerializeField] private int _actualIten;
    [SerializeField] private PickableIten[] itensInHotbar;   
    [Header("Holding and throwing")]//Holding and throwing
    public PickableIten itenYouAreHolding;
    [SerializeField] private bool _holdingTool;
    public float throwingPower;   
    //Aim
    private Ray ray;
    private RaycastHit hitInfo;
    private Vector3 direction, position;
    bool success;
    //Tool
    private Tools toolCurrentlyInUse;
    void Start()
    {
        _mainCamera = Camera.main;
        GameManager.instance.playerLogic = this;
        itensInHotbar = new PickableIten[_hotbarSize];
        try
        {
            LoadItens();
        }
        catch
        {
            Debug.Log("Inventory not yet loaded");
        }
        //Temporario
        _effectVisual = areaOfEffectAtack.GetComponent<Renderer>().material;
    }
    private void Update()
    {
        if (canMove)
        {
            InputCheck();
        }
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            Aim();
        }
    }
    public void Craft()
    {
        _stamina -= _staminaPerCraft;
    }
    public bool Raycast()
    {
        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_ray, out _hit);
        if (Vector3.Distance(transform.position, _hit.collider.transform.position) < atackRange)
        {
            return true;
        }
        return false;

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
            if (Input.GetMouseButton(0) && _holdingTool)
            {
                StartCoroutine(DealDamage());
            }
            //MouseLeftButton
            if (Input.GetMouseButtonDown(1))
            {
                InteractWithObject();
                PickUpObject();
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                if (throwingPower < 0.3f)
                {
                    DropObject();
                }
                else
                {
                    LaunchObject();
                }
            }
            if (Input.GetKey(KeyCode.Q))
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
    private void NotHoldingAnymore()
    {
        GameManager.instance.uiManager.UpdatItenSpriteInHotbar(_actualIten, 0);
        GameManager.instance.uiManager.UpdateItenNameInHotbar("");
        itenYouAreHolding = null;
        itensInHotbar[_actualIten] = null;
        _holdingTool = false;
    }
    private void InteractWithObject()
    {
        if (itenYouAreHolding != null)
        {
            if (Raycast())
            {
                if (_hit.collider.gameObject.layer == 8 || _hit.collider.gameObject.layer == 7)
                {
                    itenYouAreHolding.Drop();
                    _hit.collider.GetComponent<Storage>().TryStore(itenYouAreHolding);
                    NotHoldingAnymore();
                }
            }
        }
    }
    private void LaunchObject()
    {
        if (itenYouAreHolding != null)
        {
            itenYouAreHolding.Launch(throwingPower);
            NotHoldingAnymore();
        }
    }
    private void DropObject()
    {
        if (itenYouAreHolding != null)
        {
            itenYouAreHolding.Drop();
            NotHoldingAnymore();
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
                    if (itenYouAreHolding.IsTool)
                    {
                        _holdingTool = true;
                    }
                    else
                    {
                        _holdingTool = false;
                    }
                    GameManager.instance.uiManager.UpdatItenSpriteInHotbar(_actualIten, itenYouAreHolding.id);
                    GameManager.instance.uiManager.UpdateItenNameInHotbar(itensInHotbar[_actualIten].nameOfIten);
                }
                else
                {
                    Debug.Log("Hotbar is Full");
                }
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
                //Hide old iten before picking the new
                itensInHotbar[_actualIten].gameObject.SetActive(false);
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
            if (itensInHotbar[_actualIten] != null)
            {
                itensInHotbar[_actualIten].gameObject.SetActive(false);
            }
            _actualIten++;
            if (_actualIten >= _hotbarSize)
            {
                _actualIten = 0;
            }
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
        }
        //Update Model In Hand and Ui
        itenYouAreHolding = itensInHotbar[_actualIten];
        GameManager.instance.uiManager.UpdateHotbarItenSizes(_actualIten);
        if (itensInHotbar[_actualIten] != null)
        {
            itensInHotbar[_actualIten].gameObject.SetActive(true);
            GameManager.instance.uiManager.UpdateItenNameInHotbar(itensInHotbar[_actualIten].nameOfIten);
            if (itenYouAreHolding.IsTool)
            {
                _holdingTool = true;
            }
            else
            {
                _holdingTool = false;
            }
        }
        else
        {
            _holdingTool = false;
            GameManager.instance.uiManager.UpdateItenNameInHotbar("");
        }
    }

    public void SaveItens()
    {
        GameManager.instance.inventory.SavePlayerHotbar(itensInHotbar, _hotbarSize);
    }
    public void LoadItens()
    {
        GameManager.instance.inventory.LoadPlayerHotbar(itensInHotbar, _hotbarSize);
    }
    public void LoaditenStep(PickableIten iten, int index)
    {
        if (CheckHotbarSpace(index))
        {
            itensInHotbar[_actualIten] = iten;
            itenYouAreHolding = itensInHotbar[_actualIten];
            itenYouAreHolding.PickUp(transform, _miningTrasformPoint);
            if (itenYouAreHolding.IsTool)
            {
                _holdingTool = true;
            }
            else
            {
                _holdingTool = false;
            }
            GameManager.instance.uiManager.UpdatItenSpriteInHotbar(_actualIten, itenYouAreHolding.id);
            GameManager.instance.uiManager.UpdateItenNameInHotbar(itensInHotbar[_actualIten].nameOfIten);
        }
        else
        {
            Debug.Log("Hotbar is Full");
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
                toolCurrentlyInUse = itenYouAreHolding.GetComponent<Tools>();
                _atackTarget = _hit.collider.GetComponent<Ore>();
                _atackTarget.takeDamage(toolCurrentlyInUse.damage, toolCurrentlyInUse.power, toolCurrentlyInUse.isPickaxe);
            }
        }
        //animator.SetTrigger("Swing");
        yield return new WaitForSeconds(_atackDelay);
        //cubo muda de cor para indicar q está funcionando, remover com adição de animação
        _effectVisual.color = Color.grey;
        //---
        _stamina -= _staminaPerHit;
        GameManager.instance.uiManager.UpdateStamina(_stamina);
        _isMining = false;
    }
    private void Aim()
    {
        (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            direction = position - transform.position;
            // Ignore the height difference.
            direction.y = 0;
            // Make the transform look in the direction.
            transform.forward = direction;
        }
    }
    private (bool success, Vector3 position) GetMousePosition()
    {
        ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo, 100, _groundMask))
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
