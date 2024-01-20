using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterControl : MonoBehaviour
{
    public Slider slider;

    public void SetWater(float water)
    {
        slider.value = water;
    }

    public void SetMaxWater(float water)
    {
        slider.value = water;
        slider.maxValue = water;
    }
}
