using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TemporaryCollider : MonoBehaviour
{
    public bool isCave;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            GameManager.instance.playerLogic.SaveItens();
            if (isCave)
            {
                SceneManager.LoadScene("Hub");
            }
            else
            {
                SceneManager.LoadScene("Cave");
            }
        }
    }
}
