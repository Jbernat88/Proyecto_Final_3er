using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider HealthSlider;
    public float energyLose = 1;

    private void Start()
    {
        StartCoroutine(energyTimer());
    }

    public void SetHealth(int health)
    {
        HealthSlider.value = health;
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
