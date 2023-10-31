using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RTSHud : MonoBehaviour
{
    public GameObject[] missions = new GameObject[3];
    public TextMeshProUGUI[] missionsText = new TextMeshProUGUI[3];
    public int money = 0;
    public int[] prize = new int[3];
    public GameObject[] info = new GameObject[9];
    public void ShowInfo(int which)
    {
        info[which].SetActive(true);
    }
    public void HideInfo(int which)
    {
        info[which].SetActive(false);
    }
    public void CompleteMission(int which)
    {
        switch (which)
        {
            case 0:
                //missions
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
}
