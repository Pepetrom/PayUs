using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject pauseMenu;
    public GameObject soundPanel;
    public GameObject NPCManagerMenu, upgradeManager;
    public NPCManager NPCManager;
    public PlayerMovement playerMovement;
    public PlayerLogic playerLogic = null;
    public Inventory inventory;
    public TextMeshProUGUI moneyText, moneyTextUpgradeMenu;

    public CameraShake cameraShake;
    public int money = 0;

    private void Awake()
    {
        instance = this;
    }
    public bool UseMoney(int amount)
    {
        if(money >= amount)
        {
            money -= amount;
            moneyText.text = $"$$ {money}";
            moneyTextUpgradeMenu.text = $"$$ {money}";
            return true;
        }
        return false;
    }
    //Ui Manager
    public void SceneChange(string Scene)
    {
        Time.timeScale = 1;
        if (inventory != null)
        {
            inventory.SaveFullInventory();
        }
        SceneManager.LoadScene(Scene);
    }
    public void PauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            if (!upgradeManager.activeSelf && !NPCManagerMenu.activeSelf)
            {
                if (playerLogic != null)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        else
        {
            if (playerLogic != null)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }
    public void NpcManager()
    {
        if (NPCManagerMenu.activeSelf)
        {
            if (!upgradeManager.activeSelf && !pauseMenu.activeSelf)
            {
                if (playerLogic != null)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                NPCManagerMenu.SetActive(false);
            }
        }
        else
        {
            if (playerLogic != null)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            NPCManagerMenu.SetActive(true);
        }
    }
    public void UpgradeManager()
    {
        if (upgradeManager.activeSelf)
        {
            if(!NPCManagerMenu.activeSelf && !pauseMenu.activeSelf)
            {
                if (playerLogic != null)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }          
            upgradeManager.SetActive(false);
        }
        else
        {
            if (playerLogic != null)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            upgradeManager.SetActive(true);
        }
    }
    public void EnableSound()
    {
        soundPanel.SetActive(true);
    }
    public void DisableSound()
    {
        soundPanel.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
