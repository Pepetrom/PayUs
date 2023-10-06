using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArcadeFlappyB : MonoBehaviour
{
    [Header("Propriedades de Movimentacao")]
    [SerializeField] private float thrustForce;
    [SerializeField] private float rotationForce;

    [Header("Asas")]
    [SerializeField] private GameObject asaE;
    [SerializeField] private GameObject asaD;
    [SerializeField] private Animator animator;

    private Rigidbody rigidBody;

    private int moeda = 0;
    public Text moedasTXT;
    public float dashforce = 0;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        moeda = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inimigo"))
        {
            SceneManager.LoadScene("Derrota");
        }

        if (other.CompareTag("Moeda"))
        {
            moeda++;
            Destroy(other.gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetTrigger("Flap");
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddForce(new Vector3(Time.deltaTime * dashforce, 0, 0 ), ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddForce(new Vector3(-1 * Time.deltaTime * dashforce, 0, 0), ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustForce, ForceMode.VelocityChange);
            animator.SetTrigger("Flap");
        }

        moedasTXT.text = "Moedas: " + moeda.ToString();
    }
    /*
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -1 * rotationForce * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, 1 * rotationForce * Time.fixedDeltaTime); 
        }
    }*/
}
