using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CircularSlider : MonoBehaviour
{
    public Image fill;
    public float maxValue = 5;
    public float value = 5;
    public Image hurt;
    private void Start()
    {
        hurt.color = new Color(hurt.color.r, hurt.color.g, hurt.color.b, 0);
        fill.fillAmount = this.value / maxValue;
    }
    public void AddValue(float value)
    {
        this.value += value / maxValue;
        fill.fillAmount = this.value / maxValue;
        if (this.value < 1)
        {
            hurt.color = new Color(hurt.color.r, hurt.color.g, hurt.color.b, Mathf.Clamp((1 - (this.value * 0.33f)) / 1, 0, 0.1f));
        }
    }
    public void RemoveValue(float value)
    {
        this.value += value / maxValue;
        fill.fillAmount = this.value / maxValue;
        if (this.value < 1)
        {
            hurt.color = new Color(hurt.color.r, hurt.color.g, hurt.color.b, Mathf.Clamp((1 - (this.value * 0.33f)) / 1, 0, 0.1f));
        }
    }
}
