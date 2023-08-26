using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCylinder : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    Vector3 walkingForce;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameManager.Instance.playerBody = this;
    }
    private void FixedUpdate()
    {
        walkingForce = new Vector3(Input.GetAxis("Horizontal")* speed, rb.velocity.y, Input.GetAxis("Vertical") * speed);
        //rb.AddForce(walkingForce * speed, ForceMode.VelocityChange);
        rb.velocity = walkingForce*speed;      
    }
}
