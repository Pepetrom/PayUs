using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Slider sliderStamina, sliderFood, sliderCraft;
    [SerializeField] Image[] itenImage;
    [SerializeField] Sprite[] spriteForItem;
    [SerializeField] GameObject craft;
    Vector2 vector150 = new Vector2(150, 150), vector100 = new Vector2(100, 100);
    private void Start()
    {
        GameManager.instance.uiManager = this;
    }
    public void UpdateHotbar(int whichItemIsOn)
    {
        
        for(int i = 0; i < itenImage.Length; i++)
        {
            itenImage[i].rectTransform.sizeDelta = vector100;
        }
        
        itenImage[whichItemIsOn].rectTransform.sizeDelta = vector150;
        
    }
    public void UpdateSingleItenInHotbar(int whichItemIsOn,int ID)
    {
        itenImage[whichItemIsOn].sprite = spriteForItem[ID];
        UpdateHotbar(whichItemIsOn);
    }
    public void UpdateHungerStamina(float food, float energy)
    {
        sliderStamina.value = energy;
        sliderFood.value = food;
    }
    public void UpdateCraftingBar(float craftProgress)
    {
        if (craftProgress == 0)
        {
            sliderCraft.value = 0;
            craft.SetActive(false);
        }
        else
        {
            craft.SetActive(true);
            sliderCraft.value = craftProgress;           
        }
    }
}
