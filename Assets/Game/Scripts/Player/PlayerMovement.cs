using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    Vector3 walkingForce;
    //Movimento
    public bool canMove = true;
    private void Start()
    {
        GameManager.instance.playerMovement = this;
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            walkingForce = new Vector3(Input.GetAxis("Horizontal") * speed, rb.velocity.y, Input.GetAxis("Vertical") * speed);
            rb.velocity = walkingForce * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        //rb.AddForce(walkingForce * speed, ForceMode.VelocityChange);
    }
}
