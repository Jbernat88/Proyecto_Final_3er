using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider HealthSlider;
    public float energyLose = 1;
    public Gradient gradient;
    public Image fill;

    private void Start()
    {
        StartCoroutine(energyTimer());
    }

    public void SetHealth(int health)
    {
        HealthSlider.value = health;
        fill.color = gradient.Evaluate(HealthSlider.normalizedValue);
    }

    public IEnumerator energyTimer()
    {
        while(HealthSlider.value > 0)
        { 
            yield return new WaitForSeconds(1);
            HealthSlider.value -= energyLose;
        }
       
    }

    
}
