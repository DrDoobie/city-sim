using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool wood, stone;
    public float maxHealth, health;
    public InventoryObj inventory;
    public ItemObj item;

    void Start()
    {
        health = maxHealth;
    }

    public void Damage(float value)
    {
        if(health <= 0)
        {
            Harvest();

            return;
        }

        health -= value;
    }

    public void Harvest()
    {
        //Add resource to inventory
        inventory.AddItem(item, 1);

        this.gameObject.SetActive(false);
    }
}
