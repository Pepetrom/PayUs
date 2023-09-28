using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingMachine : MonoBehaviour
{
    [SerializeField] private Storage storage;
    void FixedUpdate()
    {
        if (storage.Itens[0] != null)
        {
            storage.Itens[0].Die();
            GameManager.instance.money += 50;
            GameManager.instance.uiManager.UpdateMoney();
        }
    }
}
