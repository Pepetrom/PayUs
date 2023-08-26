using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    private Camera mainCamera;
    public GameObject head;
    //Temporario
    public GameObject areaOfEffectAtack;
    private Material effectVisual;
    // Minerar
    public Transform miningTrasformPoint;
    public Ore atackTarget;
    public float atackRange = 1, atackDelay = 1;
    private bool mining;
    //Holding
    public PickableOre oreYouAreHolding;
    private bool holding;

    void Start()
    {
        mainCamera = Camera.main;
        GameManager.Instance.playerHead = this;
        //Temporario
        effectVisual = areaOfEffectAtack.GetComponent<Renderer>().material;
    }
    private void Update()
    {
        InputCheck();
    }
    void FixedUpdate()
    {
        Aim();
    }

    private void InputCheck()
    {
        if (Input.GetMouseButton(0) && !mining && !holding)
        {          
            StartCoroutine(DealDamage());
        }
        if (Input.GetMouseButtonDown(1) && !mining)
        {
            //dropar objeto
            if (holding)
            {
                DropObject();
            }
            //pegar objeto
            else
            {
                PickUpObject();
            }
        }
    }
    private void DropObject()
    {
        oreYouAreHolding.held = false;
        oreYouAreHolding.stored = false;
        oreYouAreHolding.Drop();
        oreYouAreHolding = null;
        holding = false;

    }
    private void PickUpObject()
    {
        Collider[] ores = Physics.OverlapSphere(miningTrasformPoint.transform.position, atackRange);
        foreach (Collider ore in ores)
        {
            // Verifica se o objeto detectado é um minério
            if (ore.CompareTag("Pickable"))
            {
                oreYouAreHolding = ore.GetComponent<PickableOre>();
                oreYouAreHolding.held = true;
                oreYouAreHolding.PickUp(transform,miningTrasformPoint);
                holding = true;
                break;
            }
        }
    }

    private IEnumerator DealDamage()
    {
        //cubo muda de cor para indicar q está funcionando, remover com adição de animação
        effectVisual.color = Color.green;
        //---
        mining = true;
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
        //animator.SetTrigger("Swing");
        yield return new WaitForSeconds(atackDelay);
        //cubo muda de cor para indicar q está funcionando, remover com adição de animação
        effectVisual.color = Color.grey;
        //---
        mining = false;
    }
    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;

            // You might want to delete this line.
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
