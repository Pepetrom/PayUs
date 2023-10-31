using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public int slotID;
    public static int itemsInCorrectSlots = 0;
    public GameObject panel;
    public PlayerLogic playerLogic = null;
    public GameObject wall;

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
                if (panel.activeSelf)
                {
                    if (playerLogic != null)
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                    Time.timeScale = 1;
                    panel.SetActive(false);
                }
                Destroy(wall.gameObject);
            }
        }
    }
}
