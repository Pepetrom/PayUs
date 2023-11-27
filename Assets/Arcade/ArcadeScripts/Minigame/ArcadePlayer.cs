using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArcadePlayer : MonoBehaviour
{
    [SerializeField] private GameObject gun, gunTip, bullet;
    [SerializeField] private float rotationForce;
    [SerializeField] private float thrustForce;
    [SerializeField] private float dashforce;
    [SerializeField] Text pontostxt;
    private Rigidbody rb;
    public float pontos = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ArcadeGameManager.instance.player = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, gunTip.transform.position, gunTip.transform.rotation);
        }
    }
    public void AddPoints()
    {
        pontos++;
        pontostxt.text = "" + pontos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inimigo"))
        {
            SceneManager.LoadScene("Derrota");
        }
        if (other.CompareTag("Obstaculo"))
        {
            SceneManager.LoadScene("Derrota");
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(0, 5 * Time.fixedDeltaTime, 0), ForceMode.VelocityChange);
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector3(0,thrustForce * Time.fixedDeltaTime, 0), ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector3(dashforce * Time.fixedDeltaTime, 0, 0), ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector3(-1 * dashforce * Time.fixedDeltaTime, 0, 0), ForceMode.VelocityChange);
        }      
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gun.transform.Rotate(0, 0, 1 * rotationForce * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gun.transform.Rotate(0, 0, -1 * rotationForce * Time.fixedDeltaTime);
        }
    }
}
