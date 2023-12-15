using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float atackRange;
    public bool canHit = true;
    RaycastHit _hit;
    Ray _ray;
    Camera _camera;
    public GameObject missionMachinePanel;
    public GameObject particula;

    public Animator pickaxeAnimator;
    public bool hasPickaxe = false;
    public bool openedNpcManager = false, hasTablet = false;
    public GameObject pickaxe;
    public GameObject openMenuTip, breakOresTip, turnOnPowerTip;
    private void Start()
    {
        _camera = Camera.main;
        GameManager.instance.playerLogic = this;
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            InputsMenu();
            TryHit();
        }
    }
    public void InputsMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.PauseMenu();
        }
        if (Input.GetKeyDown(KeyCode.Tab) && GameManager.instance.hasStarted && hasPickaxe && hasTablet)
        {
            if (!openedNpcManager)
            {
                openedNpcManager = true;
                openMenuTip.SetActive(false);
            }
            GameManager.instance.NpcManager();
        }
    }
    public void TryHit()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (canHit && hasPickaxe)
            {
                Raycast();
                if (Vector3.Distance(transform.position, _hit.collider.transform.position) < 4)
                {
                    canHit = false;
                    GameManager.instance.playerMovement.UseStamina(0.1f);
                    pickaxeAnimator.SetTrigger("Hit");
                    HitObject();
                    StartCoroutine(Hit());
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !missionMachinePanel.activeSelf)
        {
            Raycast();
            if (Vector3.Distance(transform.position, _hit.collider.transform.position) < 3)
            {
                if (_hit.collider.CompareTag("Machine") && GameManager.instance.hasStarted)
                {
                    GameManager.instance.MissionMachine();
                }
                if (_hit.collider.CompareTag("Machine") && !GameManager.instance.hasStarted)
                {
                    GameManager.instance.StartAll();
                }
            }
        }

    }
    IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.5f);
        canHit = true;
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
    public void SpawnParticula()
    {
        if (particula != null)
        {
            Instantiate(particula, _hit.point, Quaternion.identity);
        }
    }
}
