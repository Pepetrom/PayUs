using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    SurvivalTimer timer;
    public Text text;
    void Start()
    {
        timer = FindObjectOfType<SurvivalTimer>();
        Time.timeScale = 0;
        text.text = $"You Survived {timer.timer[0]}:{timer.timer[1]}:{timer.timer[2]}";
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
