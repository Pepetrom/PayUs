using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public int inventorySize;
    public int itemCount = 0;
    public int[] itemQuantity = new int[9];
    public GameObject[] items = new GameObject[9];
    public TextMeshProUGUI[] itemText = new TextMeshProUGUI[9];
    public bool isMenu = false;
    private void Start()
    {
        GameManager.instance.inventory = this;
        //LoadInventory();
        //ShowItems();
        //PlayerPrefs.SetInt("size", 10);
        //PlayerPrefs.DeleteAll();
    }
    public bool AddItems(int id)
    {
        if (itemCount < inventorySize)
        {
            itemQuantity[id]++;
            itemCount++;
            ShowItems();
            return true;
        }
        return false;
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
        if (PlayerPrefs.HasKey("size"))
        {
            inventorySize = PlayerPrefs.GetInt("size");
        }
        for (int i = 0; i < itemQuantity.Length; i ++)
        {
            PlayerPrefs.SetInt(("item" + i), itemQuantity[i]);
        }
    }
    public void SaveFullInventory()
    {
        for(int i= 0; i< itemQuantity.Length; i ++)
        {
            PlayerPrefs.SetInt(("item" + i), itemQuantity[i]);
        }
        PlayerPrefs.Save();
    }
    public void SaveInventory(string chave, int id)
    {
        PlayerPrefs.SetInt(chave, id);
        PlayerPrefs.Save();
    }
    void Awake()
    {
        instance = this;

    }

}
