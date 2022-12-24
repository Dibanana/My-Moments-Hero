using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPageUI : MonoBehaviour
{
    [SerializeField] private InventoryItemUI ItemPrefab;
    [SerializeField] RectTransform ContentPanel;

    List<InventoryItemUI> ListOfUIItems = new List<InventoryItemUI>();

    public void InitializeInventoryUI(int inventorysize)
    {
        for (int i=0; i < inventorysize; i++)
        {
            InventoryItemUI UiItem =
                Instantiate(ItemPrefab, Vector3.zero, Quaternion.identity);
            UiItem.transform.SetParent(ContentPanel);
            ListOfUIItems.Add(UiItem);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
