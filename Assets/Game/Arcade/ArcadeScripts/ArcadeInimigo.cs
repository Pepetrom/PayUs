using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeInimigo : MonoBehaviour
{
    public Transform[] targets;
    public float speed = 5.0f;
    private int currentTargetIndex = 0;

    private void FixedUpdate()
    {
        if (targets.Length > 0)
        {
            Transform currentTarget = targets[currentTargetIndex];

            Vector3 targetPosition = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
            {
                currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
            }
        }
    }
}
