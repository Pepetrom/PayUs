using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private PlayerLogic playerLogic;
    [SerializeField] private int[] id = new int[5];
    [SerializeField] private PickableIten[] allitens;
    private void Start()
    {
        if (GameManager.instance.inventory == null)
        {
            GameManager.instance.inventory = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        id = new int[5];
    }
    public void PlayerLoaded(PickableIten[] itens, int hotbarsize)
    {
        playerLogic = GameManager.instance.playerLogic;
        LoadPlayerHotbar(itens, hotbarsize);
    }
    public void SavePlayerHotbar(PickableIten[] itens, int hotbarsize)
    {
        for (int i = 0; i < id.Length; i++)
        {
            if (itens[i] == null)
            {
                id[i] = 0;
            }
            else
            {
                id[i] = itens[i].id;
            }
        }
        Debug.Log("Salvo");
    }
    public void LoadPlayerHotbar(PickableIten[] itens, int hotbarsize)
    {
        for (int i = 0; i < id.Length; i++)
        {
            if (id[i] != 0)
            {
                Debug.Log(id[i]);
                PickableIten p = Instantiate(allitens[id[i]], playerLogic.transform.position, playerLogic.transform.rotation);
                playerLogic.LoaditenStep(p, i);
                GameManager.instance.uiManager.UpdatItenSpriteInHotbar(i, p.id);
                GameManager.instance.uiManager.UpdateItenNameInHotbar(p.nameOfIten);
            }
        }
        Debug.Log("Carregado");
    }
}
