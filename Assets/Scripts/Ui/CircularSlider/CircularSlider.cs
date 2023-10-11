using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CircularSlider : MonoBehaviour
{
    public Image fill;
    public float maxValue = 5;
    public float value = 5;
    private void Start()
    {
        fill.fillAmount = this.value / maxValue;
    }
    public void AddValue(float value)
    {
        this.value += value / maxValue;
        fill.fillAmount = this.value/ maxValue;
    }
    public void RemoveValue(float value)
    {
        this.value += value / maxValue;
        fill.fillAmount = this.value / maxValue;
    }
}
