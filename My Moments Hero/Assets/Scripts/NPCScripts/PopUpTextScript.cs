using System; //This one may be useless, but I saw it in the tutorial.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //This one may be useless, but I saw it in the tutorial so I left it.
using TMPro; 

public class PopUpTextScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] PickUpText;

    //Use this for initialization - - Turns off text before arriving in range.
    private void Start(){
        foreach (GameObject Object in PickUpText)
        {
            Object.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Interaction"))
        {
            foreach (GameObject Object in PickUpText)
            { 
                Object.SetActive(true); 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Interaction"))
        {
            foreach (GameObject Object in PickUpText)
            {
                Object.SetActive(false);
            }
        }
    }
}
