using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Health Object", menuName = "Scripts/Items Scripts/Health")]
public class HealthObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Health;
    }
}
