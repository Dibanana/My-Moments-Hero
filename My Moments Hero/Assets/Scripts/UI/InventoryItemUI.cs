using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro; //(Only necessary if adding text)

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image ItemImage;
    [SerializeField] private TMP_Text NameTxt; //(I don't want quantity, but this would use the same field)
    [SerializeField] private Image BorderImage; //use this for selected item
    [SerializeField] private Image DarkenImage; //use this for already equipped item

    public event Action<InventoryItemUI> OnItemClicked, OnRightMouseBtnClick, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag;

    private bool empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }
    public void ResetData()
    {
        this.ItemImage.gameObject.SetActive(false);
        this.NameTxt.gameObject.SetActive(false);
        this.empty = true;
    }
    public void Deselect()
    {
        this.BorderImage.enabled = false;
        this.DarkenImage.enabled = false;
    }
    public void Unequip()
    {
        this.DarkenImage.enabled = false;
    }
    public void SetData(Sprite sprite, int quantity)
    {
        this.ItemImage.gameObject.SetActive(true);
        this.ItemImage.sprite = sprite;
        //this.NameTxt.text = (Item's Script Name).ItemName; (Once I learn how to add item names, I will do just that.)
        //I don't need to include the "this." on everything, the tutorial person is doing it and I'll follow him.
        this.empty = false;
    }
    public void Select()
    {
        if (BorderImage.enabled == false)
            {
                BorderImage.enabled = true;
            } else{
                //this will enable the next step of selection process
                //Create a drop down panel with 3 options (one for each weapon slot)
                //Choose which slot to equip this into and use this old slot for the replaced weapon (if it's not empty).
                DarkenImage.enabled = true;
            }

    }
    public void OnBeginDrag()
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);        //(All of this is for drag function) Allow it so the player can organize inventory, but only use clicking for equipping items.
    }
    public void OnDrop()
    {
        OnItemDroppedOn?.Invoke(this);
    }
    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }
    public void OnPointerClick(BaseEventData data)
    {
        if (empty)
            return;
        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }else{
            OnItemClicked?.Invoke(this);
        }
    }
}
