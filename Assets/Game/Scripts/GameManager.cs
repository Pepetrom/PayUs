using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMovement playerMovement;
    public PlayerLogic playerLogic;
    public UiManager uiManager;
    public int money = 0;
    //Player KeepInventory
    [SerializeField] private int[] id = new int[5];
    [SerializeField] private PickableIten[] allitens;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
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
            Debug.Log($"{i}° item trying to be loaded with the {id[i]} id");
            if (id[i] != 0)
            {
                Debug.Log(id[i]);
                playerLogic.LoaditenStep(allitens[id[i]], i);
            }
        }
        Debug.Log("Carregado");
    }
}
