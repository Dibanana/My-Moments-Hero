using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro; //(Only necessary if adding text)

public class InventoryItemUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
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
        this.NameTxt.text = "";
        this.empty = true;
    }
    public void Deselect()
    {
        this.BorderImage.enabled = false;
        //this.DarkenImage.enabled = false; (Save this function for Unequip())
    }
    public void Unequip()
    {
        this.DarkenImage.enabled = false;
    }
    
    //I genuinely don't need the int for quantity and it will be defaulted to 1.
    //However, I believe that with some creativity, I could make some creative uses to specifying weapon variants with the same or similar process.
    public void SetData(Sprite sprite, int quantity, string ItemName)
    {
        ItemImage.gameObject.SetActive(true);
        ItemImage.sprite = sprite;
        this.NameTxt.text = ItemName;
        this.empty = false;
    }
    public void Select()
    {
        if (DarkenImage.enabled == true)
        {
            DarkenImage.enabled = false;
            return;
        }
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
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);        //(All of this is for drag function) Allow it so the player can organize inventory, but only use clicking for equipping items.
    }
    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }
    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }else{
            OnItemClicked?.Invoke(this);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {

    }
}
