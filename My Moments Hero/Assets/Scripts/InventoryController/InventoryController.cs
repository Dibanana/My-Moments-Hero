using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;
using Inventory.Model;
using System;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryPageUI inventoryUI;
        [SerializeField] private InventorySO inventoryData;
        public int inventorysize = 12;

        public List<InventoryItem> initialItems = new List<InventoryItem>();
        private void Start()
        {
            PrepareUI();
            //inventoryUI.InitializeInventoryUI(inventoryData.Size);
            PrepareInventoryData();
            inventoryUI.Hide();
        }
        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach(InventoryItem Item in initialItems)
            {
                if (Item.IsEmpty)
                    continue;
                inventoryData.AddItem(Item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var Item in inventoryState)
            {
                inventoryUI.UpdateData(Item.Key, Item.Value.Item.ItemImage, Item.Value.Item.Name, Item.Value.Item.Quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            this.inventoryUI.OnSwapItems += HandleSwapItems;
            this.inventoryUI.OnStartDragging += HandleDragging;
            this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }
        private void HandleItemActionRequest(int ItemIndex)
        {
            
        }
        private void HandleDragging(int ItemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(ItemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.Item.ItemImage, inventoryItem.Item.Quantity, inventoryItem.Item.Name);
        }
        private void HandleSwapItems (int ItemIndex1, int ItemIndex2)
        {
            inventoryData.SwapItems(ItemIndex1, ItemIndex2);
        }
        private void HandleDescriptionRequest(int ItemIndex)
        {
            InventoryItem inventoryItems = inventoryData.GetItemAt(ItemIndex);
            if (inventoryItems.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO TheItemSO = inventoryItems.Item;
            inventoryUI.UpdateDescription(ItemIndex, TheItemSO.ItemImage, TheItemSO.Name, TheItemSO.Description, 
                TheItemSO.Damage, TheItemSO.Speed, TheItemSO.Knockback, inventoryItems.Quantity);

        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show();
                    foreach (var Item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(Item.Key, Item.Value.Item.ItemImage, Item.Value.Item.Name, Item.Value.Item.Quantity);
                    }
                } else
                {
                    inventoryUI.Hide();
                }
            }
        }
    }
}