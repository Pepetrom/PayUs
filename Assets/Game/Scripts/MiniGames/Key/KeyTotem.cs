using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTotem : MonoBehaviour
{
    public GameObject panel;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panel.SetActive(true);
        }
    }
}
