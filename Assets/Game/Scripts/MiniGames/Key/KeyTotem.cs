using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTotem : MonoBehaviour
{
    public GameObject panel;
    public PlayerLogic playerLogic = null;
    private bool canInteract = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.F))
        {
            KeyTotemMenu();
        }
    }

    public void KeyTotemMenu()
    {
        if (playerLogic != null)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        Time.timeScale = 0;
        panel.SetActive(true);
    }
}
