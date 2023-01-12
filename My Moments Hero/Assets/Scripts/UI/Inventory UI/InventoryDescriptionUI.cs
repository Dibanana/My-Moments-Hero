using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryDescriptionUI : MonoBehaviour
{
    [SerializeField] private DescriptionSlider SliderDMG;
    [SerializeField] private DescriptionSlider SliderSPD;
    [SerializeField] private DescriptionSlider SliderKBK;
    [SerializeField] private Image ItemImage;
    [SerializeField] private TMP_Text NameTxt;
    [SerializeField] private TMP_Text Description;
    public int DMG; //to set the damage slider
    public int SPD; //to set the speed slider
    public int KBK; //to set the knockback slider

    public void Awake()
    {
        ResetDescription();
    }
    public void ResetDescription()
    {
        this.ItemImage.gameObject.SetActive(false);
        this.NameTxt.text = "";
        this.Description.text = "";
        this.DMG = 0;
        this.SPD = 0;
        this.KBK = 0;
        SliderDMG.ResetData();
        SliderSPD.ResetData();
        SliderKBK.ResetData();
    }

    public void SetDescription(Sprite sprite, string ItemName, string ItemDescription, int Damage, int Speed, int Knockback)
    {
        this.ItemImage.gameObject.SetActive(true);
        this.ItemImage.sprite = sprite;
        this.NameTxt.text = ItemName;
        this.Description.text = ItemDescription;
        this.DMG = Damage;
        this.SPD = Speed;
        this.KBK = Knockback;
        SliderSPD.UpdateData();
        SliderDMG.UpdateData();
        SliderKBK.UpdateData();
    }
}
