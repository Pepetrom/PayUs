using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject pauseMenu;
    public GameObject soundPanel;
    
    public PlayerMovement playerMovement;
    public PlayerLogic playerLogic = null;
    public Inventory inventory;

    public CameraShake cameraShake;
    public int money = 0;

    private void Awake()
    {
        instance = this;
    }

    //Ui Manager
    public void SceneChange(string Scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Scene);
    }
    public void PauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            if (playerLogic != null)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
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
