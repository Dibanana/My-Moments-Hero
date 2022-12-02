using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Scripts/Items Scripts/Weapon")]
public class WeaponObject : ItemObject
{
    public float ATK;
    public float DEF;
    public void Awake()
    {
        type = ItemType.Weapon;
    }
}
