using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] IsEquipped;
    public InventoryObject inventory; //allows the script to access inventory
    // Update is called once per frame

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
