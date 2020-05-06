using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Food")]
public class FoodObj : ItemObj
{
    public int restoreHealthValue;

    public void Awake()
    {
        type = ItemType.Food;
    }
}
