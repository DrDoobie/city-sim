using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObj : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    public ItemDatabaseObject database;
    public List<InventorySlot> container = new List<InventorySlot>();

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
        //Binary formatter in jason utility

        //Serialize scriptable object into a string

        //Binary formatter in file stream to create file and save stream in given locatoin

        string saveData = JsonUtility.ToJson(this, true);

        
    }

    public void Load()
    {
        //Load inventory
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
