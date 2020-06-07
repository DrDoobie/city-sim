using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObj : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    public List<InventorySlot> container = new List<InventorySlot>();

    private ItemDatabaseObject database;

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        database = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }

    public void AddItem(ItemObj _item, int _amount)
    {
        for(int i = 0; i < container.Count; i++)
        {
            if(container[i].item == _item)
            {
                container[i].AddAmount(_amount);

                return;
            }
        }

        container.Add(new InventorySlot(database.getID[_item], _item, _amount));
    }

    public void Save()
    {
        //Binary formatter in json utility

        //Serialize scriptable object into a string

        //Binary formatter in file stream to create file and save stream in given locatoin
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);

        file.Close();
    }

    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath))) //If there is a save file to load from
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);

            file.Close();
        }
    }

    public void OnAfterDeserialize()
    {
        for(int i = 0; i < container.Count; i++)
        {
            container[i].item = database.getItem[container[i].id];
        }
    }

    public void OnBeforeSerialize()
    {
    }   
}

[System.Serializable]
public class InventorySlot
{
    public int id;
    public ItemObj item;
    public int amount;

    public InventorySlot(int _id, ItemObj _item, int _amount)
    {
        id = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
