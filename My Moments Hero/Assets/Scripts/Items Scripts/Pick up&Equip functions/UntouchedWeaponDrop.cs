using System; //This one may be useless, but I saw it in the tutorial.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //This one may be useless, but I saw it in the tutorial so I left it.
using TMPro; 

public class UntouchedWeaponDrop : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI PickUpText;
    //Use this for initialization - - Turns off text before arriving in range.

    [SerializeField]private TextMeshProUGUI InventoryFullText;

    public ItemObject item; //lets all other codes know that the object attatched to this line of code is an item
    public InventoryObject inventory; //allows the script to access inventory
    private bool PickUpAllowed = false;
    public int InventoryCapacity;
    public int InventoryAmount;

    private void Start(){
        PickUpText.gameObject.SetActive(false);
        InventoryFullText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    private void Update()
    {
        if (PickUpAllowed ==true && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        InventoryCapacity = inventory.InventoryCapacity;
        InventoryAmount = inventory.InventoryAmount;
        if(collision.gameObject.name.Equals("Player"))
        {
            if (InventoryAmount < InventoryCapacity)
            {
                PickUpText.gameObject.SetActive(true);
                PickUpAllowed = true;
            } else{
                InventoryFullText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            PickUpText.gameObject.SetActive(false);
            InventoryFullText.gameObject.SetActive(false);
            PickUpAllowed = false;
        }
    }
    void Pickup()
    {
        var item=this.GetComponent<Item>();
        if (InventoryAmount < InventoryCapacity)
        {
            inventory.AddItem(item.item); //adds the item to inventory. (hopefully it's properly defined.)
            Destroy(gameObject);
        }
    }
}
