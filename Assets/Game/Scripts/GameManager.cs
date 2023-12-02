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
    public GameObject missionMachine;
    public GameObject NPCManagerMenu, upgradeManager;
    public NPCManager NPCManager;
    public PlayerMovement playerMovement;
    public PlayerLogic playerLogic = null;
    public Inventory inventory;
    public TextMeshProUGUI moneyText, moneyTextUpgradeMenu;
    public CameraShake cameraShake;
    public int money = 0;
    public int openMenuCount = 0;
    private void Awake()
    {
        instance = this;
    }
    public bool UseMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            moneyText.text = $"$$ {money}";
            moneyTextUpgradeMenu.text = $"$$ {money}";
            return true;
        }
        return false;
    }
    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = $"$$ {money}";
        moneyTextUpgradeMenu.text = $"$$ {money}";
    }
    //Ui Manager
    public void SceneChange(string Scene)
    {
        Time.timeScale = 1;
        if (inventory != null)
        {
            inventory.SaveFullInventory();
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(Scene);
    }
    public void MenuCounter(int i)
    {
        openMenuCount += i;
        if(openMenuCount <= 0)
        {
            if (playerLogic != null)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            if (playerLogic != null)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    public void PauseMenu()
    {
        if (pauseMenu.activeSelf)
        {            
            MenuCounter(-1);
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        else
        {
            MenuCounter(1);
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }
    public void NpcManager()
    {
        if (NPCManagerMenu.activeSelf)
        {
            MenuCounter(-1);
            NPCManagerMenu.SetActive(false);
        }
        else
        {
            MenuCounter(1);
            NPCManagerMenu.SetActive(true);
        }
    }
    public void UpgradeManager()
    {
        if (upgradeManager.activeSelf)
        {
            MenuCounter(-1);
            upgradeManager.SetActive(false);
        }
        else
        {
            MenuCounter(1);
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
    public void MissionMachine()
    {
        if (missionMachine.activeSelf)
        {
            MenuCounter(-1);
            missionMachine.SetActive(false);
        }
        else
        {
            MenuCounter(1);
            missionMachine.SetActive(true);
        }
    }
}
