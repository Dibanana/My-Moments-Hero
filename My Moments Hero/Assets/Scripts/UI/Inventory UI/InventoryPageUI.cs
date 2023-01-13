using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI 
//By having a namespace, it restricts the use of the code for only instances when the programmer makes a "using ..." at the beginning.
//This apparently assists in organizing code and preventing the code from being provided or called upon in places it really shouldn't... I think.
{
    public class InventoryPageUI : MonoBehaviour
    {
        [SerializeField] private InventoryItemUI ItemPrefab;
        [SerializeField] RectTransform ContentPanel;
        [SerializeField] private InventoryDescriptionUI ItemDescription;
        [SerializeField] private MouseFollower mouseFollower;

        List<InventoryItemUI> ListOfUIItems = new List<InventoryItemUI>();

        public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
        //for my understanding once I translate these to my personal descriptions:
        //OnDescriptionRequested = OnSelected
        //OnItemActionRequested = OnEquip
        //OnStartDragging = OnBeginDrag

        public event Action<int, int> OnSwapItems;

        [Header ("Weapon Stats")] 
        //Weapon class should have stats, then this will act as the middleman to tell the description what the stats are
        public int Damage;

        internal void ResetAllItems()
        {
            foreach(var Item in ListOfUIItems)
            {
                Item.ResetData();
                Item.Deselect();
            }
        }

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
        public void UpdateDescription(int ItemIndex, Sprite ItemImage, string NameTxt, string Description, int Damage, int Speed, int Knockback, int Quantity)
        {
            ItemDescription.SetDescription(ItemImage, NameTxt, Description, Damage, Speed, Knockback, Quantity);
            DeselectAllItems();
            ListOfUIItems[ItemIndex].Select();
        }
        public void UpdateData(int ItemIndex, Sprite ItemImage, string ItemName, int Quantity)
        {
            if (ListOfUIItems.Count > ItemIndex)
            {
                ListOfUIItems[ItemIndex].SetData(ItemImage, Quantity, ItemName);
            }
        }
        private void HandleShowItemActions(InventoryItemUI UIInventoryItem) //1 (order) //Occurs when Right Clicked
        {
            int Index = ListOfUIItems.IndexOf(UIInventoryItem);
            if (Index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(Index);
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
            HandleItemSelection(UIInventoryItem);
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
        public void CreateDraggedItem(Sprite sprite, int Quantity, string NameTxt)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, Quantity, NameTxt);
        }
        private void HandleItemSelection(InventoryItemUI UIInventoryItem)  //5 When Item is left Clicked
        {
            int Index = ListOfUIItems.IndexOf(UIInventoryItem);
            if (Index == -1)
                return;
            OnDescriptionRequested?.Invoke(Index);
            //UIInventoryItem.Select();
        }
        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }
        public void ResetSelection()
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
}