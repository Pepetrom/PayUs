using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class NPCManager : MonoBehaviour
{
    public NPC[] npcs = new NPC[3];
    private int[] NPCSelectedJob = new int[3];
    public Image[] jobImage = new Image[3];
    public Sprite[] jobs = new Sprite[3];
    int actual = 0;
    public int jobTime;
    public Transform[] holes = new Transform[3];
    public Transform baseReturn;
    public int fuel;
    public int fuelConsumption;
    public Slider fuelSlider;
    //Upgrades
    public GameObject[] buttons = new GameObject[12];
    public int[] value = new int[12];
    public TextMeshProUGUI[] valueTexts = new TextMeshProUGUI[12];
    public bool[] upgrades = new bool[12];
    public int[] itemMultiplier = new int[3];
    public int[] orevalue = new int[3];
    public void Porta()
    {

    }
    private void Start()
    {
        GameManager.instance.NPCManager = this;
        //PlayerPrefs.DeleteAll();
        //SaveAll();
        StartFunction();
        fuelSlider.value = fuel;
    }
    public void StartFunction()
    {
        LoadAll();
        LoadValues();
        for (int i = 0; i < 3; i++)
        {
            npcs[i].StartNPC(NPCSelectedJob[i]);
            jobImage[i].sprite = jobs[NPCSelectedJob[i]];
        }
    }
    private void SaveAll()
    {
        for(int i = 0; i< NPCSelectedJob.Length; i++)
        {
            PlayerPrefs.SetInt("jobs"+ i, NPCSelectedJob[i]);
        }

        PlayerPrefs.SetInt("fuel", fuel);

        PlayerPrefs.SetInt("consumption", fuelConsumption);


            PlayerPrefs.SetInt("jobTime", jobTime);


        for (int i = 0; i < upgrades.Length; i++)
        {
            if(upgrades[i] == true)
            {
                PlayerPrefs.SetInt("upgrades" + i, 1);
            }
            else
            {
                PlayerPrefs.SetInt("upgrades" + i, 0);
            }
        }

        PlayerPrefs.SetInt("Money", GameManager.instance.money);

        PlayerPrefs.Save();
    }
    private void LoadValues()
    {
        GameManager.instance.UseMoney(0);
        for(int i = 0; i < upgrades.Length; i++)
        {
            buttons[i].SetActive(upgrades[i]);
        }
        for(int i=0;i<valueTexts.Length; i++)
        {
            valueTexts[i].text = "$$ "+ value[i];
        }
    }
    public void Upgrade(int which)
    {
        if (GameManager.instance.UseMoney(value[which]))
        {          
            upgrades[which] = true;
            buttons[which].SetActive(upgrades[which]);
            switch (which)
            {
                case 0:
                    Porta();
                    break;
                case 1:
                    fuelConsumption = 2;
                    break;
                case 2:
                    jobTime = 10;
                    break;
                case 3:
                    itemMultiplier[0] = 2;
                    break;
                case 4:
                    Porta();
                    break;
                case 5:
                    fuel = 16;
                    break;
                case 6:
                    itemMultiplier[1] = 2;
                    break;
                case 7:
                    orevalue[0] = 3;
                    break;
                case 8:
                    Porta();
                    break;
                case 9:
                    itemMultiplier[2] = 2;
                    break;
                case 10:
                    orevalue[1] = 6;
                    break;
                case 11:
                    orevalue[2] = 9;
                    break;
            }
        }
            SaveAll();
    }
    private void LoadAll()
    {
        for (int i = 0; i < NPCSelectedJob.Length; i++)
        {
            NPCSelectedJob[i] = PlayerPrefs.GetInt("jobs" + i);
        }

        fuel = PlayerPrefs.GetInt("fuel");

        fuelConsumption = PlayerPrefs.GetInt("consumption");


            jobTime = PlayerPrefs.GetInt("jobTime");


        for (int i = 0; i < upgrades.Length; i++)
        {
            if (PlayerPrefs.GetInt("upgrades") == 1)
            {
                upgrades[i] = true;
                buttons[i].SetActive(true);
            }
            else
            {
                upgrades[i] = false;
            }
        }

       GameManager.instance.money = PlayerPrefs.GetInt("Money");
    }
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
    public bool UseFuel()
    {
        if (fuel >= fuelConsumption)
        {
            fuel -= fuelConsumption;
            fuelSlider.value = fuel;
            return true;
        }
        return false;
    }
    public void WhichNPC(int npc)
    {
        actual = npc;
    }
    public void SelectJob(int job)
    {
        npcs[actual].ChangeJob(job);
        NPCSelectedJob[actual] = job;
        jobImage[actual].sprite = jobs[NPCSelectedJob[actual]];
        SaveAll();
    }
}
