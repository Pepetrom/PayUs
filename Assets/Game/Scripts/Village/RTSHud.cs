using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RTSHud : MonoBehaviour
{
    //Missions
    public GameObject[] missions = new GameObject[3];
    public TextMeshProUGUI[] missionsText = new TextMeshProUGUI[3];
    public TextMeshProUGUI[] missionsRewardText = new TextMeshProUGUI[3];
    public int[] mQuantity = new int[3], mType = new int[3];
    public int[] mPrize = new int[3];
    public GameObject textCannotDo;
    public string[] resourceNames;
    //Resoucers Icons
    public GameObject[] info = new GameObject[9];
    //Money
    public int money = 0;
    public TextMeshProUGUI moneyText;
    //VillagerBuyer
    public GameObject villagerAsset;
    public Transform villagerSpawn;
    private void Start()
    {
        RandomQuest(0);
        RandomQuest(1);
        RandomQuest(2);
    }
    public void ShowInfo(int which)
    {
        info[which].SetActive(true);
    }
    public void HideInfo(int which)
    {
        info[which].SetActive(false);
    }
    public void RandomQuest(int which)
    {
        mType[which] = Random.Range(0,11);
        mQuantity[which] = Random.Range(1, 5);
        mPrize[which] = mQuantity[which] * 2;
        missionsText[which].text = $"{resourceNames[mType[which]]} x {mQuantity[which]}" ;
        missionsRewardText[which].text = "Reward: " + mPrize[which];

    }
    public void ShowMoney()
    {
        moneyText.text = "Money: " + money;
    }
    public void AddVillager()
    {
        if(money > 10)
        {
            Instantiate(villagerAsset, villagerSpawn.transform.position, villagerSpawn.transform.rotation);
            money -= 10;
            ShowMoney();
        }
    }
    public void CompleteMission(int which)
    {
        if (GameManager.instance.inventory.itemQuantity[mType[which]] >= mQuantity[which])
        {
            GameManager.instance.inventory.itemQuantity[mType[which]] -= mQuantity[which];
            money += mPrize[which];
            ShowMoney();
            RandomQuest(which);
        }
        else
        {
            StartCoroutine(ShowHideText());
        }
    }
    private IEnumerator ShowHideText()
    {
        textCannotDo.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        textCannotDo.SetActive(false);
    }
}
