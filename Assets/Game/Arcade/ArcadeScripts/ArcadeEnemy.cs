using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeEnemy : MonoBehaviour
{
    public Transform target;
    public float thrustForce;

    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position;
        Vector3 direction = targetPosition - transform.position;

        // Adiciona uma força na direção do alvo
        rigidBody.AddRelativeForce(direction * thrustForce * Time.fixedDeltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
