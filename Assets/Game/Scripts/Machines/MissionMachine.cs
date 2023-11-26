using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionMachine : MonoBehaviour
{
    public TextMeshProUGUI[] missionValueText = new TextMeshProUGUI[3];
    public TextMeshProUGUI[] oreText = new TextMeshProUGUI[3];
    public GameObject[] buttons = new GameObject[3];
    string[] oreNames = new string[3] { "Coopa", "Irona", "Silva" };
    int[] missionValue = new int[3];
    int[] oreType = new int[3];
    int[] quantity = new int[3];
    bool[] completed = new bool[3];
    float timer;
    public Slider timerSlider;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
                quantity[i] = Random.Range(1, 11);
                oreType[i] = Random.Range(0, 3);
                missionValue[i] = quantity[i] * (oreType[i] + 1);
                buttons[i].SetActive(true);
                missionValueText[i].text = $"Pays: ${missionValue[i]}";
                oreText[i].text = $"{quantity[i]} {oreNames[oreType[i]]}";
        }
    }
    void UpdateMissions()
    {
        for(int i =0;i < 3; i++)
        {
            if (completed[i])
            {
                quantity[i] = Random.Range(1,11);
                oreType[i] = Random.Range(0, 3);
                missionValue[i] = quantity[i] * (oreType[i]+1);
                buttons[i].SetActive(true);
                missionValueText[i].text = $"Pays: ${missionValue[i]}";
                oreText[i].text = $"{quantity[i]} {oreType[i]}";
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerSlider.value = timer;
        if (timer >= 30)
        {
            UpdateMissions();
            timer = 0;
        }
    }
    public void MissionOne()
    {
        if (GameManager.instance.inventory.itemQuantity[oreType[0]] >= quantity[0])
        {
            GameManager.instance.inventory.itemQuantity[oreType[0]] -= quantity[0];
            GameManager.instance.AddMoney(missionValue[0]);
            completed[0] = true;
            buttons[0].SetActive(false);
        }
    }
    public void MissionTwo()
    {
        if (GameManager.instance.inventory.itemQuantity[oreType[1]] >= quantity[1])
        {
            GameManager.instance.inventory.itemQuantity[oreType[1]] -= quantity[1];
            GameManager.instance.AddMoney(missionValue[1]);
            completed[1] = true;
            buttons[1].SetActive(false);
        }
    }
    public void MissionThree()
    {
        if (GameManager.instance.inventory.itemQuantity[oreType[2]] >= quantity[2])
        {
            GameManager.instance.inventory.itemQuantity[oreType[2]] -= quantity[2];
            GameManager.instance.AddMoney(missionValue[2]);
            completed[2] = true;
            buttons[2].SetActive(false);
        }
        
    }
}
