using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public int job;
    public float timeForMove = 2;
    public NavMeshAgent ai;
    int tempJob;
    private void Start()
    {
        ai = GetComponent<NavMeshAgent>();       
    }
    public void StartNPC(int job)
    {
        ChangeJob(job);
        StartCoroutine(DoJob());
    }
    public void ChangeJob(int job)
    {
        this.job = job;
    }
    private IEnumerator DoJob()
    {
        if(GameManager.instance.NPCManager.UseFuel())
        {
            tempJob = job;
            ai.SetDestination(GameManager.instance.NPCManager.holes[job].position);
            yield return new WaitForSeconds(timeForMove + GameManager.instance.NPCManager.jobTime);
            ai.SetDestination(GameManager.instance.NPCManager.baseReturn.position);
            yield return new WaitForSeconds(timeForMove);
            GameManager.instance.inventory.AddItems(tempJob);
            StartCoroutine(DoJob());
        }
        else
        {
            yield return new WaitForSeconds(10);
            StartCoroutine(DoJob());
        }
        
    }
}
