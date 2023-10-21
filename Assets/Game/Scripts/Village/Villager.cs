using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Villager : MonoBehaviour
{
    PlayerRTS playerRTS;
    int professionAtual = 0;
    public GameObject[] profession = new GameObject[3];
    public Outline outline;
    public GameObject[] selectionArrow = new GameObject[3];
    public Order sent, actual;
    public List<Order> orders;
    NavMeshAgent navMesh;
    void Start()
    {
        outline = profession[professionAtual].GetComponent<Outline>();
        profession[0].SetActive(true);
        navMesh = GetComponent<NavMeshAgent>();
        playerRTS = PlayerRTS.instance;
        orders.Add(sent);
    }
    public void ChangeProfession(int which)
    {
        profession[professionAtual].SetActive(false);
        profession[which].SetActive(true);
        outline = profession[professionAtual].GetComponent<Outline>();
    }
    public void ShowOutline()
    {
        outline.enabled = true;
    }
    public void HideOutline()
    {
        outline.enabled = false;
    }
    public void Select()
    {
        selectionArrow[professionAtual].SetActive(true);
    }
    public void Deselect()
    {
        selectionArrow[professionAtual].SetActive(false);
    }
    public void Command(Vector3 start, Vector3 end, Type type)
    {
        actual = Instantiate(sent);
        actual.owner = this;
        actual.returnPoint = end;
        actual.objectivePoint = start;
        actual.type = type;
        orders.Add(actual);
        if (orders.Count <3)
        {
            actual.Started();
        }
        Deselect();
    }
    public void SetDestination(Vector3 destiny)
    {
        navMesh.destination = destiny;
    }
}
