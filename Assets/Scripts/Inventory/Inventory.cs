using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public static InventoryItem carriedItem;

    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] InventorySlot[] hotbarSlots;

    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] Item[] items;

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    //Set Inventory Size
    public int inventorySize;
    [SerializeField] Sprite[] inventorySlotSprites;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("size", inventorySize);
        LoadInventory();
    }
    public void LoadInventory()
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {       
            if(PlayerPrefs.HasKey("slot" + i))
            {
                SpawnInventoryItemOnLoad(items[PlayerPrefs.GetInt("slot" + i)],i);
            }
        }
        if (PlayerPrefs.HasKey("size"))
        {
            inventorySize = PlayerPrefs.GetInt("size");
        }
        for (int i = 0; i < inventorySlots.Length && i < inventorySize; i++)
        {
            inventorySlots[i].GetComponent<Image>().sprite = inventorySlotSprites[0];
        }
    }
    public void RemoveFromSaveInventory(string chave, int id)
    {
        PlayerPrefs.DeleteKey(chave);
        PlayerPrefs.Save();
    }
    public void SaveFullInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].itemInSlot != null)
            {
                PlayerPrefs.SetInt(("slot" + i), inventorySlots[i].itemInSlot.myItem.id);
            }
            else
            {
                PlayerPrefs.DeleteKey("slot" + i);
            //PlayerPrefs.SetInt(("slot" + i), 0);
            }
            
        }
        PlayerPrefs.SetInt("size", inventorySize);
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
        giveItemBtn.onClick.AddListener( delegate { SpawnInventoryItem(); } );
    }

    void Update()
    {
        if(carriedItem == null) return;

        carriedItem.transform.position = Input.mousePosition;
    }
    
    public void SetCarriedItem(InventoryItem item)
    {
        if(carriedItem != null)
        {
            if(item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
            item.activeSlot.SetItem(carriedItem);
        }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
    }

    public void SpawnInventoryItem(Item item = null)
    {
        Item _item = item;
        if(_item == null)
        { _item = PickRandomItem(); }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            // Check if the slot is empty
            if(inventorySlots[i].itemInSlot == null)
            {
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i]);
                SaveInventory("slot" + i, inventorySlots[i].itemInSlot.myItem.id);
                break;
            }
        }
    }
    public void SpawnInventoryItemOnLoad(Item item, int slot)
    {
        // Check if the slot is empty
        if (inventorySlots[slot].itemInSlot == null)
        {
            Instantiate(itemPrefab, inventorySlots[slot].transform).Initialize(item, inventorySlots[slot]);
        }

    }

    Item PickRandomItem()
    {
        int random = Random.Range(0, items.Length);
        return items[random];
    }
}
