using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;
using Inventory.Model;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryPageUI inventoryUI;
        [SerializeField] private InventorySO inventoryData;
        public int inventorysize = 12;

        private void Start()
        {
            PrepareUI();
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            //inventoryData.Initialize();
            inventoryUI.Hide();
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

        }
        private void HandleSwapItems (int ItemIndex1, int ItemIndex2)
        {

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
                TheItemSO.Damage, TheItemSO.Speed, TheItemSO.Knockback);

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
                        inventoryUI.UpdateData(Item.Key, Item.Value.Item.ItemImage, Item.Value.Item.Name);
                    }
                } else
                {
                    inventoryUI.Hide();
                }
            }
        }
    }
}