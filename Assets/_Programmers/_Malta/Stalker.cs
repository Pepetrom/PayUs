using UnityEngine;
using UnityEngine.AI;

public class Stalker : MonoBehaviour
{
    public Transform[] waypoints; 
    private int currentWaypointIndex = 0; 
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); 
        SetDestinationToNextWaypoint();
    }

    private void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            SetDestinationToNextWaypoint();
        }
    }

    private void SetDestinationToNextWaypoint()
    {
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}

