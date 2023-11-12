using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public int[] itemQuantity = new int[3];
    public GameObject[] items = new GameObject[3];
    public TextMeshProUGUI[] itemText = new TextMeshProUGUI[3];
    public bool isMenu = false;
    
    private void Start()
    {
        GameManager.instance.inventory = this;
        LoadInventory();
        ShowItems();
    }
    public bool AddItems(int id)
    {
        itemQuantity[id] += GameManager.instance.NPCManager.itemMultiplier[id];
        ShowItems();
        SaveFullInventory();
        return true;
    }
    public void ShowItems()
    {
        for (int i = 0; i < itemQuantity.Length; i++)
        {
            if (isMenu)
            {
                itemText[i].text = "" + itemQuantity[i];
            }
            else
            {

                if (itemQuantity[i] > 0)
                {
                    items[i].gameObject.SetActive(true);
                    itemText[i].text = "" + itemQuantity[i];
                }
                else
                {
                    items[i].gameObject.SetActive(false);
                }
            }
        }
    }
    public void LoadInventory()
    {
        for (int i = 0; i < itemQuantity.Length; i ++)
        {
            itemQuantity[i] =  PlayerPrefs.GetInt("item" + i);
        }
        ShowItems();
    }
    public void SaveFullInventory()
    {
        for(int i= 0; i< itemQuantity.Length; i ++)
        {
            PlayerPrefs.SetInt(("item" + i), itemQuantity[i]);
        }
        PlayerPrefs.Save();
    }
    public void SaveInventory(string chave, string value)
    {
        PlayerPrefs.SetString(chave, value);
        PlayerPrefs.Save();
    }
    void Awake()
    {
        instance = this;

    }

}
