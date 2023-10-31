using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyTotem : MonoBehaviour
{
    public GameObject panel;
    public PlayerLogic playerLogic = null;
    private bool canInteract = false;
    public GameObject pressF;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pressF.SetActive(true);
            canInteract = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pressF.SetActive(false);
            canInteract = false;
        }
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.F))
        {
            KeyTotemMenu();
            pressF.SetActive(false);
            canInteract = false;
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
