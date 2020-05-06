using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Tool,
    Weapon,
    Food,
    Default
}

public abstract class ItemObj : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)] public string description;
}
