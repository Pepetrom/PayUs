using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotTag { None, Head, Chest, Legs, Feet }

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem itemInSlot { get; set; }
    //public InventoryItem item;

    public SlotTag myTag;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(Inventory.carriedItem == null) return;
            if(myTag != SlotTag.None && Inventory.carriedItem.myItem.itemTag != myTag) return;
            SetItem(Inventory.carriedItem);
            Inventory.instance.SaveFullInventory();
        }
    }
    public void SetItem(InventoryItem item)
    {
        Inventory.carriedItem = null;

        // Reset old slot
        item.activeSlot.itemInSlot = null;

        // Set current slot
        itemInSlot = item;
        itemInSlot.activeSlot = this;
        itemInSlot.transform.SetParent(transform);
        itemInSlot.canvasGroup.blocksRaycasts = true;
    }
}
