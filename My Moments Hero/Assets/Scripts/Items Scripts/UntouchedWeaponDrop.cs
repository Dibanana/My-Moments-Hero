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

    private bool PickUpAllowed;

    //Use this for initialization - - Turns off text before arriving in range.
    private void Start(){
        PickUpText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    private void Update()
    {
        if (PickUpAllowed && Input.GetKeyDown(KeyCode.E))
            PickUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("Player"))
        {
            PickUpText.gameObject.SetActive(true);
            PickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            PickUpText.gameObject.SetActive(false);
            PickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        Destroy(gameObject);
    }
}
