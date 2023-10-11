using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text txtHoursAndMinutes, txtDays, txtNextDay;
    float seconds = 0,minutes = 0, hours = 20, days = 1, nextPaymentDay = 2;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        if(seconds >= 5)
        {
            minutes += 1;
            if (minutes >= 60)
            {
                minutes = 0;
                hours++;
                if (hours >= 24)
                {
                    days++;
                    hours = 0;
                    if (days >= nextPaymentDay)
                    {
                        SceneManager.LoadScene("Defeat");
                    }
                    txtDays.text = $"Day {days}";
                }
            }
            if(minutes < 10)
            {
                if(hours < 10)
                {
                    txtHoursAndMinutes.text = $"0{hours}:0{minutes}";
                }
                else
                {
                    txtHoursAndMinutes.text = $"{hours}:0{minutes}";
                }
            }
            else
            {
                if (hours < 10)
                {
                    txtHoursAndMinutes.text = $"0{hours}:{minutes}";
                }
                else
                {
                    txtHoursAndMinutes.text = $"{hours}:{minutes}";
                }
            }
            seconds = 0;
        }
        
    }
}
