using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUIScript : MonoBehaviour
{
    [SerializeField] private Health SPHealth;
    public TMP_Text CurrentHealth;
    public TMP_Text MaxHealth;
    public Slider Slider;
    public void Start()
    {
        Slider.value = SPHealth.MaxHealth;
        MaxHealth.text = "/"+SPHealth.MaxHealth.ToString();
    }
    public void OnSliderChanged(float Value){
        CurrentHealth.text = Value.ToString();
    }
    //Changes the value of a text to match the float of the slider.

    public void UpdateHealth()
    {
        Slider.value = SPHealth.health;
        OnSliderChanged(Slider.value);
    }
}   //Gathers information from another script to update the value of the slider.
