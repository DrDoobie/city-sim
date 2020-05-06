using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Tool")]
public class ToolObj : ItemObj
{
    public int attackBonus, defenseBonus;

    public void Awake()
    {
        type = ItemType.Tool;
    }
}

