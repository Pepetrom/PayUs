using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public float fuel;
    public float fuelConsumption;
    public Slider fuelSlider;
    //Upgrades
    //public GameObject[] buttons = new GameObject[12];
    public Button[] buttons;
    public int[] value = new int[12];
    public TextMeshProUGUI[] valueTexts = new TextMeshProUGUI[12];
    public bool[] upgrades = new bool[12];
    public int[] itemMultiplier = new int[3];
    public int[] orevalue = new int[3];
    public GameObject[] doors = new GameObject[3];

    public bool firstFoodBuy = true;
    public GameObject[] upgradeButton;
    public GameObject[] npcfaces;

    public void Porta(GameObject door)
    {
        door.gameObject.SetActive(false);
    }
    public void DisableButton(Button upgrade)
    {
        upgrade.interactable = false;
    }
    public void EnableButton(Button upgrade)
    {
        upgrade.interactable = true;
    }
    private void Start()
    {
        GameManager.instance.nPCManager = this;
        PlayerPrefs.DeleteAll();
        SaveAll();
       // LoadAll();
    }
    public void StartFunction()
    {
        LoadAll();
        GameManager.instance.inventory.LoadInventory();
        GameManager.instance.inventory.ShowItems();
    }
    private void SaveAll()
    {
        for(int i = 0; i< NPCSelectedJob.Length; i++)
        {
            PlayerPrefs.SetInt("jobs"+ i, NPCSelectedJob[i]);
        }

        PlayerPrefs.SetFloat("fuel", fuel);

        PlayerPrefs.SetFloat("consumption", fuelConsumption);


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
    public void Upgrade(int which)
    {
        if (GameManager.instance.UseMoney(value[which]))
        {          
            switch (which)
            {
                case 0:
                    Porta(doors[0]);
                    for (int i = 0; i < 3; i++)
                    {
                        npcs[i].StartNPC(NPCSelectedJob[i]);
                        jobImage[i].sprite = jobs[NPCSelectedJob[i]];
                    }
                    for (int i = 0; i < npcfaces.Length; i++)
                    {
                        npcfaces[i].SetActive(true);
                    }
                    EnableButton(buttons[1]);
                    EnableButton(buttons[2]);
                    EnableButton(buttons[4]);
                    EnableButton(buttons[8]);
                    break;
                case 1:
                    orevalue[0] = 2;
                    EnableButton(buttons[3]);
                    DisableButton(buttons[2]);
                    break;
                case 2:
                    itemMultiplier[0] = 2;
                    EnableButton(buttons[3]);
                    DisableButton(buttons[1]);
                    break;
                case 3:
                    fuelConsumption = 0.5f;                   
                    break;

                case 4:
                    Porta(doors[1]);                   
                    EnableButton(buttons[5]);
                    EnableButton(buttons[6]);
                    break;
                case 5:
                    orevalue[1] = 6;
                    EnableButton(buttons[7]);
                    DisableButton(buttons[6]);
                    break;
                case 6:                  
                    itemMultiplier[1] = 2;
                    EnableButton(buttons[7]);
                    DisableButton(buttons[5]);
                    break;
                case 7:
                    jobTime = 10;
                    break;

                case 8:
                    Porta(doors[2]);
                    Porta(doors[3]);
                    EnableButton(buttons[9]);
                    EnableButton(buttons[10]);
                    break;
                case 9:
                    orevalue[2] = 9;
                    EnableButton(buttons[11]);
                    DisableButton(buttons[10]);
                    break;
                case 10:
                    itemMultiplier[2] = 2;
                    EnableButton(buttons[11]);
                    DisableButton(buttons[9]);
                    break;
                case 11:                   
                    fuel = 16;
                    break;
            }
            DisableButton(buttons[which]);
        }
            //SaveAll();
    }
    public void LoadAll()
    {      
        /*
        //JobsTime
        jobTime = PlayerPrefs.GetInt("jobTime");

        //Fuel
        fuel = PlayerPrefs.GetInt("fuel");
        fuelSlider.value = fuel;

        //FuelConsumption
        fuelConsumption = PlayerPrefs.GetInt("consumption");
        
        //Upgrades

        for (int i = 0; i < upgrades.Length; i++)
        {
            if (PlayerPrefs.GetInt("upgrades" + i) == 1)
            {
                upgrades[i] = true;
                buttons[i].SetActive(true);
            }
            else
            {
                upgrades[i] = false;
            }
        }
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(upgrades[i]);
        }
        */
        for (int i = 0; i < valueTexts.Length; i++)
        {
            valueTexts[i].text = "$$ " + value[i];
        }

        //Money
        //GameManager.instance.money = PlayerPrefs.GetInt("Money");
        GameManager.instance.UseMoney(0);
        /*
        //Jobs
        for (int i = 0; i < NPCSelectedJob.Length; i++)
        {
            NPCSelectedJob[i] = PlayerPrefs.GetInt("jobs" + i);
        }
        */
        
    }
    public void StartNpcs()
    {       
        for(int i = 0; i < upgradeButton.Length; i++)
        {
            upgradeButton[i].SetActive(true);
        }
        
    }
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        SaveAll();
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
    public void BuyFood(int food)
    {
        if(fuel < 10)
        {
            if (GameManager.instance.UseMoney(food * 3))
            {
                if (firstFoodBuy)
                {
                    firstFoodBuy = false;
                    StartNpcs();
                }
                fuel = 10;
                fuelSlider.value = fuel;
            }
        }      
    }
}
