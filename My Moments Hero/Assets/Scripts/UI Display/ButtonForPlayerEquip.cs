using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonForPlayerEquip : MonoBehaviour
{
    public InventoryObject playerInventory; //access inventory
    public InventoryObject PlayerEquip; //access equipped items
    public bool IsEquipped = false;
    public int InventoryCapacity;
    public int InventoryAmount;
    public int EquipCapacity;
    public int EquipAmount;
    public ItemObject item;

    public void DefineItem()
    {
        //Debug.Log("var item = ", this.GetComponent<Item>().item);
        //This will define the item so that the inventory will know what to take out... But how do I define it?
    }
    
    public void IsEquipping()
    {
        InventoryCapacity = playerInventory.InventoryCapacity;
        InventoryAmount = playerInventory.InventoryAmount;
        EquipCapacity = PlayerEquip.InventoryCapacity;
        EquipAmount = PlayerEquip.InventoryAmount;
        if (IsEquipped == true)
        {
            if(InventoryCapacity > InventoryAmount)
            {
                UnEquipped();
                IsEquipped = false;
            } else{
                //make a message for INVENTORY IS FULL
                //if anything, the inventory should never be full, but this is just in case the future has a change in design
            }
        }
        if (IsEquipped == false)
        {
            if (EquipCapacity > EquipAmount)
            {
                Equipped();
                IsEquipped = true;
            } else{
                //Do something to swap an equipped item with the new item... Maybe I can choose a slot to replace? I'll consider it eventually.
            }
        }
    }
    private void Equipped()
    {
        var item=this.GetComponent<Item>();
        PlayerEquip.AddItem(item.item); //adds item to equipped items
        playerInventory.RemoveItem(item.item); //should remove the item from inventory. (hopefully it's properly defined.)        
    }
    private void UnEquipped()
    {
        var item=this.GetComponent<Item>();
        playerInventory.AddItem(item.item);
        PlayerEquip.RemoveItem(item.item);
    }
}