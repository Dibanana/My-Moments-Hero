using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryPageUI inventoryUI;

    public int inventorysize = 12;

    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorysize);
        inventoryUI.Hide();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
            } else
            {
                inventoryUI.Hide();
            }
        }
    }
}
