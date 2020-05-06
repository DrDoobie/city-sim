using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObj : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();

    public void AddItem(ItemObj _item, int _amount)
    {
        bool hasItem = false;

        for(int i = 0; i < container.Count; i++)
        {
            if(container[i].item == _item)
            {
                container[i].AddAmount(_amount);

                hasItem = true;

                break;
            }
        }

        if(!hasItem)
        {
            container.Add(new InventorySlot(_item, _amount));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObj item;
    public int amount;

    public InventorySlot(ItemObj _item, int _amount)
    {
        item = _item;

        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
