using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string SavePath;
    private ItemDatabaseObject Database;
    public List<InventorySlot> Container = new List<InventorySlot>();
    
    private void OnEnable()
    {
#if UNITY_EDITOR
        Database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        Database = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }

    public void AddItem(ItemObject _item)
    {
        for (int i=0; i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                return;
            }
        }
        Container.Add(new InventorySlot(Database.GetId[_item], _item));
    }

    public void Save()
    {
        string SaveData = JsonUtility.ToJson(this,true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, SavePath));
        bf.Serialize(file, SaveData);
        file.Close();
    }
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, SavePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =File.Open(string.Concat(Application.persistentDataPath, SavePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    public void OnAfterDeserialize()
    {
        //for (int i = 0; i < Container.Count; i++)
            //Container[i].item = Database.GetItem[Container[i].ID];
                             //This code is used to save the amount of the item held.
    }
    public void OnBeforeSerialize()
    {}
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public ItemObject item;
    public InventorySlot(int _id, ItemObject _item)
    {
        ID = _id;
        item = _item;
    }
}