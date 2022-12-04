using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] IsEquipped;
    public InventoryObject inventory; //allows the script to access inventory
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            inventory.Load();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
