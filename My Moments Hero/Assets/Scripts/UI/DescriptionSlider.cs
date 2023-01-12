using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionSlider : MonoBehaviour
{
    [SerializeField] private InventoryDescriptionUI Description;
    public Slider Slider;
    [SerializeField] int StatVar; //Manually use this to determine which stat this is affected by (1=DMG, 2=SPD, 3=KBK)
    public void ResetData()
    {
        Slider.value = 0;
    }
    public void UpdateData()
    {
        if (StatVar == 1)
            Slider.value = Description.DMG;
        if (StatVar == 2)
            Slider.value = Description.SPD;
        if (StatVar == 3)
            Slider.value = Description.KBK;
    }
}
