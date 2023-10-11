using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeSpaceShipControl : MonoBehaviour
{
    [Header("Propriedades de Movimentacao")]
    [SerializeField] private float thrustForce;
    [SerializeField] private float rotationForce;


    Rigidbody r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            r.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -1 * rotationForce * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, 1 * rotationForce * Time.fixedDeltaTime);
        }
    }
}
