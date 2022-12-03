using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryObject inventory;
    public bool[] IsEquipped;
    //private bool PickUpAllowed = false;

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item); //adds the item to inventory.
            Destroy(other.gameObject);
            StartCoroutine(Cooldown());  //This part is most likely irrelevant, but I added it and the enumerator cooldown myself to avoid duplicate pickup glitches. If anyone looks at this later, feel free to delete.
        }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
