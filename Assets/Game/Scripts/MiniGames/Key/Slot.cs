using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public int slotID;
    public static int itemsInCorrectSlots = 0;
    public GameObject panel;

    public void OnDrop(PointerEventData eventData)
    {
        DragAndDrop item = eventData.pointerDrag.GetComponent<DragAndDrop>();
        if (item != null && item.itemID == slotID)
        {
            Debug.Log("Cu");
            itemsInCorrectSlots++;

            item.transform.SetParent(transform);
            item.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            if (itemsInCorrectSlots == 3)
            {
                panel.SetActive(false);
            }
        }
    }
}
