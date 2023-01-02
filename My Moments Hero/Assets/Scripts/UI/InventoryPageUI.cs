using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPageUI : MonoBehaviour
{
    [SerializeField] private InventoryItemUI ItemPrefab;
    [SerializeField] RectTransform ContentPanel;
    [SerializeField] private InventoryDescriptionUI ItemDescription;
    [SerializeField] private MouseFollower mouseFollower;

    List<InventoryItemUI> ListOfUIItems = new List<InventoryItemUI>();

    //public Sprite Image, Image2;
    //public int quantity;
    //public string ItemName, ItemName2;
    //public string Description;
    //Was told to add, then was removed mid way through tutorial

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
    //for my understanding once I translate these to my personal descriptions:
    //OnDescriptionRequested = OnSelected
    //OnItemActionRequested = OnEquip
    //OnStartDragging = OnBeginDrag

    public event Action<int, int> OnSwapItems;

    [Header ("Weapon Stats")] 
    //Weapon class should have stats, then this will act as the middleman to tell the description what the stats are
    public int Damage;
    public int Speed;
    public int Knockback;
    //I still need to figure out how to portray premade stats through sliders. I know I can do it, but need to know how.

    private int CurrentlyDraggedItemIndex = -1;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        ItemDescription.ResetDescription();
    }

    public void InitializeInventoryUI(int inventorysize)
    {
        for (int i=0; i < inventorysize; i++)
        {
            InventoryItemUI UiItem =
                Instantiate(ItemPrefab, Vector3.zero, Quaternion.identity);
            UiItem.transform.SetParent(ContentPanel);
            ListOfUIItems.Add(UiItem);
            UiItem.OnItemClicked += HandleItemSelection;
            UiItem.OnItemBeginDrag += HandleBeginDrag;
            UiItem.OnItemDroppedOn += HandleSwap;
            UiItem.OnItemEndDrag += HandleEndDrag;
            UiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    public void UpdateData(int ItemIndex, Sprite ItemImage, string NameTxt) //includes int itemquantity, but I dont want quantity
    {
        if (ListOfUIItems.Count > ItemIndex)
        {
            ListOfUIItems[ItemIndex].SetData(ItemImage, 1, NameTxt);
        }
    }
    private void HandleShowItemActions(InventoryItemUI UIInventoryItem) //1 (order) //Occurs when Right Clicked
    {
        //Debug.Log(uiinventoryitem.name);
        ListOfUIItems[0].Deselect();
        ItemDescription.ResetDescription();
    }
    private void HandleEndDrag(InventoryItemUI UIInventoryItem) //2 //Self-explanatory
    {
        ResetDraggedItem();
    }
    private void HandleSwap(InventoryItemUI UIInventoryItem) //3 //When item dragged and dropped over another slot
    {
        int Index = ListOfUIItems.IndexOf(UIInventoryItem);
        if (Index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(CurrentlyDraggedItemIndex, Index);
    }
    private void ResetDraggedItem() //When item is dragged and dropped 
    {
        mouseFollower.Toggle(false);
        CurrentlyDraggedItemIndex = -1;
    }
    private void HandleBeginDrag(InventoryItemUI UIInventoryItem) //4
    {
        int Index = ListOfUIItems.IndexOf(UIInventoryItem);
        if (Index == -1)
            return;
        CurrentlyDraggedItemIndex = Index;
        HandleItemSelection(UIInventoryItem);
        OnStartDragging?.Invoke(Index);
    }
    public void CreateDraggedItem(Sprite sprite, string NameTxt)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, 1, NameTxt);
    }
    private void HandleItemSelection(InventoryItemUI UIInventoryItem)  //5
    {
        int Index = ListOfUIItems.IndexOf(UIInventoryItem);
        if (Index == -1)
            return;
        OnDescriptionRequested?.Invoke(Index);
    }
    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }
    private void ResetSelection()
    {
        ItemDescription.ResetDescription();
        DeselectAllItems();
    }
    private void DeselectAllItems()
    {
        foreach (InventoryItemUI Item in ListOfUIItems)
        {
            Item.Deselect();
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
