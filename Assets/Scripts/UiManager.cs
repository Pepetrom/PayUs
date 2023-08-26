using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Slider sliderStamina, sliderFood;
    private void Start()
    {
        GameManager.Instance.uiManager = this;
    }
    public void UpdateUi(float food, float energy)
    {
        sliderStamina.value = energy;
        sliderFood.value = food;
    }
}
