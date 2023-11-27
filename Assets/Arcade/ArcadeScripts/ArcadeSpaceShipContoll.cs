using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeSpaceShipContoll : MonoBehaviour
{
    [Header("Propriedades de Movimentacao")]
    [SerializeField] private float thrustForce;
    [SerializeField] private float rotationForce;

    [Header("Propulsores")]
    [SerializeField] private GameObject propulsorEsquerdo;
    [SerializeField] private GameObject propulsorMeio;
    [SerializeField] private GameObject propulsorDireito;

    [Header("Trem de Pouso")]
    [SerializeField] private GameObject tdp;

    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        propulsorEsquerdo.SetActive(false);
        propulsorMeio.SetActive(false);
        propulsorDireito.SetActive(false);
        tdp.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inimigo"))
        {
            SceneManager.LoadScene("Arcade");
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            propulsorMeio.SetActive(true);
            tdp.SetActive(false);
        }
        else
        {
            propulsorMeio.SetActive(false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            propulsorEsquerdo.SetActive(true);
        }
        else
        {
            propulsorEsquerdo.SetActive(false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            propulsorDireito.SetActive(true);
        }
        else
        {
            propulsorDireito.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime, ForceMode.Impulse);
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
