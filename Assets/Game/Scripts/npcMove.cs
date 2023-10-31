using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public List<Transform> destinationPoints;
    private int currentDestinationIndex = 0;
    public float botSpeed = 3.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNextDestination();
        agent.speed = botSpeed;
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            SetNextDestination();
        }
    }

    void SetNextDestination()
    {
        if (destinationPoints.Count == 0)
        {
            return;
        }

        agent.SetDestination(destinationPoints[currentDestinationIndex].position);
        currentDestinationIndex = (currentDestinationIndex + 1) % destinationPoints.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            Destroy(other.gameObject);
        }
    }
}
