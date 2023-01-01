using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPageUI : MonoBehaviour
{
    [SerializeField] private InventoryItemUI ItemPrefab;
    [SerializeField] RectTransform ContentPanel;
    [SerializeField] private InventoryDescriptionUI ItemDescription;
    [SerializeField] private MouseFollower mouseFollower;

    List<InventoryItemUI> ListOfUIItems = new List<InventoryItemUI>();

    public Sprite Image, Image2;
    //public int quantity;
    public string ItemName, ItemName2;
    public string Description;

    [Header ("Weapon Stats")] 
    //Weapon class should have stats, then this will act as the middleman to tell the description what the stats are
    public int Damage;
    public int Speed;
    public int Knockback;
    //I still need to figure out how to portray premade stats through sliders. I know I can do it, but need to know how.

    private int CurrentlyDraggedItemIndex = -1;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
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
    private void HandleShowItemActions(InventoryItemUI UIInventoryItem) //1 (order)
    {
        //Debug.Log(uiinventoryitem.name);
        ListOfUIItems[0].Deselect();
        ItemDescription.ResetDescription();
    }
    private void HandleEndDrag(InventoryItemUI UIInventoryItem) //2
    {
            mouseFollower.Toggle(false);
    }
    private void HandleSwap(InventoryItemUI UIInventoryItem) //3
    {
        int Index = ListOfUIItems.IndexOf(UIInventoryItem);
        if (Index == -1)
        {
            mouseFollower.Toggle(false);
            CurrentlyDraggedItemIndex = -1;
            return;
        }
        ListOfUIItems[CurrentlyDraggedItemIndex].SetData(Index ==0? Image:Image2,1, ItemName:ItemName2);
        ListOfUIItems[Index].SetData(CurrentlyDraggedItemIndex == 0? Image : Image2, 1, ItemName:ItemName2);
        mouseFollower.Toggle(false);
        CurrentlyDraggedItemIndex = -1;
    }
    private void HandleBeginDrag(InventoryItemUI UIInventoryItem) //4
    {
        int Index = ListOfUIItems.IndexOf(UIInventoryItem);
        if (Index == -1)
            return;
        CurrentlyDraggedItemIndex = Index;
        mouseFollower.Toggle(true);
        mouseFollower.SetData(Index == 0? Image : Image2, 1, ItemName:ItemName2);
    }
    private void HandleItemSelection(InventoryItemUI UIInventoryItem)  //5
    {
        ItemDescription.SetDescription(Image, ItemName, Description, Damage, Speed, Knockback);
        ListOfUIItems[0].Select();
    }
    public void Show()
    {
        gameObject.SetActive(true);
        ItemDescription.ResetDescription();

        ListOfUIItems[0].SetData(Image, 1, ItemName); //Sets up an inventory item in the first slot (Just a debug tool, delete when finished)
        ListOfUIItems[1].SetData(Image2, 1, ItemName2);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
