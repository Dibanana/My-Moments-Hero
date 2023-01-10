using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<InventoryItem> inventoryItems;
    [field: SerializeField] public int Size {get; private set;} = 12;

    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;
    public void Initialize()
    {
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }
    public void AddItem(ItemSO Item, int Quantity)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if(inventoryItems[i].IsEmpty)
            {
                inventoryItems[i] = new InventoryItem
                {
                    Item = Item,
                    Quantity = Quantity,
                };
            }
        }
    }
    public void AddItem(InventoryItem Item)
    {
        AddItem(Item.Item, 1); //Item.Quantity instead of 1 if it doesn't work
    }
    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue=
            new Dictionary<int, InventoryItem>();
        for (int i = 0; i <inventoryItems.Count; i++)
        {
            if(inventoryItems[i].IsEmpty)
                continue;
            returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }

    public InventoryItem GetItemAt(int ItemIndex)
    {
        return inventoryItems[ItemIndex];    
    }

    public void SwapItems(int itemIndex1, int itemIndex2)
    {
        InventoryItem item1 = inventoryItems[itemIndex1];
        inventoryItems[itemIndex1] = inventoryItems[itemIndex2];
        inventoryItems[itemIndex2] = item1;
        InformAboutChange();
    }

    private void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
    }
}
[Serializable]

public struct InventoryItem
{
    public int Quantity;
    public ItemSO Item;

    public bool IsEmpty => Item == null;

    public InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem
        {
            Item = this.Item, Quantity = newQuantity,
        };
    }
    public static InventoryItem GetEmptyItem()
    => new InventoryItem
    {
        Item = null,
        Quantity = 0,
    };
}
