using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] GameObject inventoryScreen;
    [SerializeField] Animator animator;
    [SerializeField] float atackRange;
    public bool canHit = true;
    RaycastHit _hit;
    Ray _ray;
    Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
        GameManager.instance.playerLogic = this;
    }

    void Update()
    {
        OpenInventory();
        TryHit();
    }
    public void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryScreen.activeSelf)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
                inventoryScreen.SetActive(false);
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                inventoryScreen.SetActive(true);            
            }          
        }
    }
    public void TryHit()
    {
        //Play animation
        if (Input.GetKey(KeyCode.Mouse0) && canHit)
        {
            canHit = false;
            animator.SetTrigger("Hit");
            GameManager.instance.playerMovement.UseStamina(0.1f);
        }
    }
    public bool Raycast()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_ray, out _hit);
        if(_hit.collider != null)
        {
            if (Vector3.Distance(transform.position, _hit.collider.transform.position) < atackRange)
            {
                return true;
            }
        }     
        return false;
    }
    public void HitObject()
    {
        if (Raycast())
        {
            if (_hit.collider.CompareTag("Breakable"))
            {
                _hit.collider.GetComponent<Breakable>().TakeHit(1);
            }
        }   
    }
}
