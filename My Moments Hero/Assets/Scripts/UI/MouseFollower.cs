using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] InventoryItemUI Item;

    public void Awake()
    {
        canvas = transform.root.GetComponentInChildren<Canvas>();
        Item = GetComponentInChildren<InventoryItemUI>();
    }

    public void SetData(Sprite sprite, int quantity, string ItemName)
    {
        Item.SetData(sprite, quantity, ItemName);
    }

//It may do well to explain the next process to the best of my ability (Better than nothing at least)
//There is a difference between the computer screen's coordinates, and in-world coordinates.
//The computer mouse only exists in the computer screen's space, so trying to find it's location in-world would be difficult.
//While at the same time, RectTransform only works through the computer screen's coordinates as well, so by having 
//RectTransform constantly following the mouse, it would need constant updates to translate the position to the 
//in-world coordinates as well.//
    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform, 
            Input.mousePosition, 
            canvas.worldCamera,
            out position
                );
        transform.position= canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        //Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}
