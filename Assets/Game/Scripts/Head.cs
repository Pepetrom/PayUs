using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Head : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    private Camera mainCamera;
    [SerializeField] GameObject head;
    //Temporario
    public GameObject areaOfEffectAtack;
    private Material effectVisual;
    //Movimento
    public bool canMove = true;
    //Mining
    [SerializeField] Transform miningTrasformPoint;
    [SerializeField] Ore atackTarget;
    [SerializeField] float atackRange = 1, atackDelay = 1;
    private bool mining;
    //Holding
    public PickableOre oreYouAreHolding;
    private bool holding, justPickedTheItem;
    //RayCast
    Vector3 foward = new Vector3(5,0,0);
    private RaycastHit hit;
    private Ray ray;
    [SerializeField] LayerMask layerForRaycast;
    public bool Holding
    {
    get { return holding; }
    }
public float power;
    //Stamina Food
    private float stamina = 1, food = 1, regenCooldown = 0;
    [SerializeField] private float staminaPerHit,staminaPerCraft, foodSpentPerTick, staminaGainedPerTick;

    void Start()
    {
        mainCamera = Camera.main;
        GameManager.Instance.playerHead = this;
        //Temporario
        effectVisual = areaOfEffectAtack.GetComponent<Renderer>().material;
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
        stamina -= staminaPerCraft;
        canMove = true;
    }
    public bool Raycast()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        if (Vector3.Distance(transform.position, hit.collider.transform.position)< 2)
        {
            return true;
        }
        return false;
        
    }
    private void RegenStamina()
    {
        if (regenCooldown < 1)
        {
            regenCooldown += Time.deltaTime;
        }
        else
        {
            if (stamina < 1 && food > 0)
            {
                stamina += staminaGainedPerTick;
                food -= foodSpentPerTick;
                regenCooldown = 0;
                GameManager.Instance.uiManager.UpdateUi(food, stamina);
            }
        }
    }
    private void InputCheck()
    {
        if (Input.GetMouseButton(0) && !mining && !holding && stamina > 0)
        {
            StartCoroutine(DealDamage());
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (justPickedTheItem)
            {
                justPickedTheItem = false;
            }
            else
            {
                if (power < 0.5f)
                {
                    DropObject();
                }
                else
                {
                    LaunchObject();
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && !mining)
        {
            if (!holding)
            {
                PickUpObject();
                justPickedTheItem = true;
            }
            else
            {
                justPickedTheItem = false;
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (power < 1)
            {
                power += Time.deltaTime;
            }
        }
        else
        {
            power = 0;
        }
    }
    private void LaunchObject()
    {
        oreYouAreHolding.Launch(power);
        oreYouAreHolding = null;
        holding = false;
    }
    public void PlaceInStation()
    {
        oreYouAreHolding.Drop();
        oreYouAreHolding = null;
        holding = false;
        justPickedTheItem = false;
    }
    private void DropObject()
    {
        if (oreYouAreHolding != null)
        {
            oreYouAreHolding.Drop();
            oreYouAreHolding = null;
            holding = false;
        }
    }
    private void PickUpObject()
    {
        if (Raycast())
        {
            if (hit.collider.CompareTag("Pickable"))
            {
                oreYouAreHolding = hit.collider.GetComponent<PickableOre>();
                oreYouAreHolding.PickUp(transform, miningTrasformPoint);
                holding = true;
            }
        }
    }
    

    private IEnumerator DealDamage()
    {
        //cubo muda de cor para indicar q está funcionando, remover com adição de animação
        effectVisual.color = Color.green;
        //---
        mining = true;
        if (Raycast())
        {
            if (hit.collider.CompareTag("Ore"))
            {
                atackTarget = hit.collider.GetComponent<Ore>();
                atackTarget.takeDamage(1);
            }
        }

        /*
        Collider[] ores = Physics.OverlapSphere(miningTrasformPoint.transform.position, atackRange);
        foreach (Collider ore in ores)
        {
            // Verifica se o objeto detectado é um minério
            if (ore.CompareTag("Ore"))
            {
                atackTarget = ore.GetComponent<Ore>();
                atackTarget.takeDamage(1);
                //break;
            }
        }
        */

        //animator.SetTrigger("Swing");
        yield return new WaitForSeconds(atackDelay);
        //cubo muda de cor para indicar q está funcionando, remover com adição de animação
        effectVisual.color = Color.grey;
        //---
        stamina -= staminaPerHit;
        GameManager.Instance.uiManager.UpdateUi(food, stamina);
        mining = false;
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
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, 100, groundMask))
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
