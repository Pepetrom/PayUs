using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickTablet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.StartTab();
            GameManager.instance.playerLogic.hasTablet = true;
            GameManager.instance.playerLogic.breakOresTip.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
