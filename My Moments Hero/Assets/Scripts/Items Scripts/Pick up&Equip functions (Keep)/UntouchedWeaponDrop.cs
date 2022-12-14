using System; //This one may be useless, but I saw it in the tutorial.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //This one may be useless, but I saw it in the tutorial so I left it.
using TMPro; 

public class UntouchedWeaponDrop : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI PickUpText;
    [SerializeField]private TextMeshProUGUI InventoryFullText;

    private bool PickUpAllowed = false;    //Use this for initialization - - Turns off text before arriving in range.
    //public int InventoryCapacity;
    //public int InventoryAmount;
    //for personal use later

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
        //InventoryCapacity = inventory.InventoryCapacity;
        //InventoryAmount = inventory.InventoryAmount;
        if(collision.gameObject.name.Equals("Player"))
        {
            //if (InventoryAmount < InventoryCapacity)
            //{
            PickUpText.gameObject.SetActive(true);
            PickUpAllowed = true;
        } //else{
           //     InventoryFullText.gameObject.SetActive(true);
            //}
        //}
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
        //Do whatever the tutorial says here.
    }
}
