using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Slider sliderStamina, sliderFood, sliderCraft;
    [SerializeField] GameObject craft;
    private void Start()
    {
        GameManager.Instance.uiManager = this;
    }
    public void UpdateUi(float food, float energy)
    {
        sliderStamina.value = energy;
        sliderFood.value = food;
    }
    public void CraftingBar(float craftProgress)
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
