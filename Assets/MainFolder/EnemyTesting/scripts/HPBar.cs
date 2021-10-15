using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider sliderHP;

    public void SetMaxHealth(int Maxhealth)
    {
        sliderHP.maxValue = Maxhealth;
        sliderHP.value = Maxhealth;
    }

    public void SetHealth(int health)
    {
        sliderHP.value = health;
    }
}
