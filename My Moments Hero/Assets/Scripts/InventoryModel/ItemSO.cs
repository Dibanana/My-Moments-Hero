using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    //A lot of this will pertain to item quantity. However, I don't want quantity amounts so a lot of this will go unutilized.
    [field: SerializeField] public bool IsStackable {get; set;}
    
    public int ID => GetInstanceID();

    [field: SerializeField] public int MaxStackSize{get; set;} =1;
    [field: SerializeField] public string Name{get; set;}
    [field: SerializeField] [field: TextArea] public string Description {get; set;}
    [field: SerializeField] public Sprite ItemImage {get; set;}
    
}
