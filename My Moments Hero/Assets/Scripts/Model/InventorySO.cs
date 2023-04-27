using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<InventoryItem> inventoryItems;
    [field: SerializeField] public int Size {get; private set;} = 12; //I have no idea how to serialize or use this field, but this directly influences how many slots are present in the inventory

    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;
    public void Initialize()
    {
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }
    public int AddItem(ItemSO Item, int Quantity)
    {
        if(Item.IsStackable == false)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                while (Quantity>0 && IsInventoryFull() == false)
                {
                    Quantity -= AddItemToFirstFreeSlot(Item, 1);
                    
                }
                InformAboutChange();
                return Quantity;
            }
        }
        Quantity = AddStackableItem(Item, Quantity);
        InformAboutChange();
        return Quantity;
    }
    private int AddItemToFirstFreeSlot(ItemSO Item, int Quantity)
    {
        InventoryItem newItem = new InventoryItem
        {
            Item = Item,
            Quantity = Quantity
        };
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                inventoryItems[i] = newItem;
                return Quantity;
            }
        }
        return 0;
    }
    private bool IsInventoryFull()
        => inventoryItems.Where(Item => Item.IsEmpty).Any() == false;
    
    private int AddStackableItem (ItemSO Item, int Quantity)
    {
        for (int i = 0; i< inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
                continue;
            if(inventoryItems[i].Item.ID == Item.ID)
            {
                int AmountPossibleToTake =
                    inventoryItems[i].Item.MaxStackSize - inventoryItems[i].Quantity;
                if (Quantity> AmountPossibleToTake)
                {
                    inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].Item.MaxStackSize);
                    Quantity -= AmountPossibleToTake;
                } else
                {
                    inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].Quantity+Quantity);
                    InformAboutChange();
                    return 0;
                }
            }
        }
        while(Quantity>0 && IsInventoryFull()==false)
        {
            int newQuantity = Mathf.Clamp(Quantity,0,Item.MaxStackSize);
            Quantity -= newQuantity;
            AddItemToFirstFreeSlot(Item, newQuantity);
        }
        return Quantity;
    }
    public void AddItem(InventoryItem Item)
    {
        AddItem(Item.Item, Item.Quantity); //Manually make sure you ensure that inventory quantity is above 1
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
        if (itemIndex1 <= -1)
            return;
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
