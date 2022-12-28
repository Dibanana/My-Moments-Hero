using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPageUI : MonoBehaviour
{
    [SerializeField] private InventoryItemUI ItemPrefab;
    [SerializeField] RectTransform ContentPanel;
    [SerializeField] private InventoryDescriptionUI ItemDescription;

    List<InventoryItemUI> ListOfUIItems = new List<InventoryItemUI>();

    public Sprite Image;
    //public int quantity;
    public string NameTxt;
    public string Description;

    [Header ("Weapon Stats")] 
    //Weapon class should have stats, then this will act as the middleman to tell the description what the stats are
    public int Damage;
    public int Speed;
    public int Knockback;
    //I still need to figure out how to portray premade stats through sliders. I know I can do it, but need to know how.

    private void Awake()
    {
        Hide();
        ItemDescription.ResetDescription();
    }

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
            UiItem.OnItemDroppedOn += HandleSwap;
            UiItem.OnItemEndDrag += HandleEndDrag;
            UiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }
    private void HandleItemSelection(InventoryItemUI obj)
    {
        ItemDescription.SetDescription(Image, NameTxt, Description, Damage, Speed, Knockback);
        ListOfUIItems[0].Select();
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
        //Debug.Log(obj.name);
        ListOfUIItems[0].Deselect();
    }
    public void Show()
    {
        gameObject.SetActive(true);
        ItemDescription.ResetDescription();

        ListOfUIItems[0].SetData(Image, 1); //Sets up an inventory item in the first slot (Just a debug tool, delete when finished)
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
