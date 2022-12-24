using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
//using TMPro; (Only necessary if adding quantity text)

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image ItemImage;
    //[SerializeField] private TMP_Text quantityTxt; (I don't want quantity, but this would probably be useful another time)
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
        //this.quantityTxt.text = quantity + ""; 
        //I don't need to include the "this." on everything, the tutorial person is doing it and I'll follow him.
        this.empty = false;
    }
    public void Select()
    {
        if (empty)
            return;
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
        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }else{
            OnItemClicked?.Invoke(this);
        }
    }
}
