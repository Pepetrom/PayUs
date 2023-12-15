using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxePickerTemp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        GameManager.instance.playerLogic.pickaxe.SetActive(true);
        GameManager.instance.playerLogic.hasPickaxe = true;
            GameManager.instance.playerLogic.breakOresTip.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
