using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObj[] items;
    public Dictionary<ItemObj, int> getID = new Dictionary<ItemObj, int>();
    public Dictionary<int, ItemObj> getItem = new Dictionary<int, ItemObj>();

    public void OnAfterDeserialize()
    {
        getID = new Dictionary<ItemObj, int>();
        getItem = new Dictionary<int, ItemObj>();

        for(int i = 0; i < items.Length; i++)
        {
            getID.Add(items[i], i);
            getItem.Add(i, items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
    }   
}
