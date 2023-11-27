using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeBullet : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public ArcadePlayer player;
    float timer = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inimigo"))
        {
            Destroy(other.gameObject);
            ArcadeGameManager.instance.player.AddPoints();
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Obstaculo"))
        {
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5)
        {
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(speed, 0, 0));
    }
}
