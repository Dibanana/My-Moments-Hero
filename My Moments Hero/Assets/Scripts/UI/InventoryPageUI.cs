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
            UiItem.OnItemClicked += HandleItemSelection;
            UiItem.OnItemBeginDrag += HandleBeginDrag;
            UiItem.OnItemClicked += HandleSwap;
            UiItem.OnItemClicked += HandleEndDrag;
            UiItem.OnItemClicked += HandleShowItemActions;
        }
    }
    private void HandleItemSelection(InventoryItemUI obj)
    {
        
    }
    private void HandleBeginDrag(InventoryItemUI obj)
    {
        
    }
    private void HandleSwap(InventoryItemUI obj)
    {
        
    }
    private void HandleEndDrag(InventoryItemUI obj)
    {
        
    }
    private void HandleShowItemActions(InventoryItemUI obj)
    {
        Debug.Log(obj.name);
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
