using System;
using UnityEngine.UI;

[Serializable]
public class HealthBar
{
    public Slider slider;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        
    }
}
