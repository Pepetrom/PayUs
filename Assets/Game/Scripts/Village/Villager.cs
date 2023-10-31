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
    public GameObject[] profession = new GameObject[4];
    public GameObject selectionArrow;
    public Order sent, actual;
    public List<Order> orders;
    NavMeshAgent navMesh;

    public GameObject tempSelectArrow;

    void Start()
    {
        profession[3].SetActive(true);
        navMesh = GetComponent<NavMeshAgent>();
        playerRTS = PlayerRTS.instance;
        orders.Add(sent);
    }
    public void ChangeProfession(int which)
    {
        profession[professionAtual].SetActive(false);
        profession[which].SetActive(true);
    }
    public void ShowOutline()
    {
        tempSelectArrow.SetActive(true);
    }
    public void HideOutline()
    {
        tempSelectArrow.SetActive(false);
    }
    public void Select()
    {
        selectionArrow.SetActive(true);
    }
    public void Deselect()
    {
        selectionArrow.SetActive(false);
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
