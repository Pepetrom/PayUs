using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Slider sliderStamina, sliderOxigen, sliderCraft;
    [SerializeField] Image[] itenImage;
    [SerializeField] GameObject craft;
    [SerializeField] Sprite[] spriteForItem;
    [SerializeField] TMP_Text itenInHotbarName;
    
    Vector2 sizeSpriteHotBarHovered = new Vector2(140, 140), sizeSpriteHotBar = new Vector2(100, 100);
    private void Start()
    {
        GameManager.instance.uiManager = this;
    }
    public void UpdateHotbarItenSizes(int whichItemIsOn)
    {
        
        for(int i = 0; i < itenImage.Length; i++)
        {
            itenImage[i].rectTransform.sizeDelta = sizeSpriteHotBar;
        }
        
        itenImage[whichItemIsOn].rectTransform.sizeDelta = sizeSpriteHotBarHovered;
    }    
    public void UpdatItenSpriteInHotbar(int whichItemIsOn,int ID)
    {
        itenImage[whichItemIsOn].sprite = spriteForItem[ID];
        UpdateHotbarItenSizes(whichItemIsOn);        
    }
    public void UpdateItenNameInHotbar(string name)
    {
        itenInHotbarName.text = name;
    }
    public void UpdateStamina(float energy)
    {
        sliderStamina.value = energy;
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
