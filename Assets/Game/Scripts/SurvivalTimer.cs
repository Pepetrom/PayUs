using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalTimer : MonoBehaviour
{
    public float miliseconds = 0, seconds=0, minutes = 0, hours = 0;
    public string[] timer = new string[3];
    public Text screenTimer;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        miliseconds += Time.deltaTime;
        if(miliseconds >= 1)
        {
            miliseconds = 0;
            seconds++;
        }
        if(seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }
        if (minutes >= 60)
        {
            minutes = 0;
            hours++;
        }
        if (seconds >= 10)
        {
            timer[2] = "" + seconds;
        }
        else
        {
            timer[2] = "0" + seconds;
        }
        if (minutes >= 10)
        {
            timer[1] = "" + minutes;
        }
        else
        {
            timer[1] = "0" + minutes;
        }
        if (hours >= 10)
        {
            timer[0] = "" + hours;
        }
        else
        {
            timer[0] = "0" + hours;
        }
        if (screenTimer != null)
        {
            screenTimer.text = $"{timer[0]}:{timer[1]}:{timer[2]}";   
        }
    }
}
