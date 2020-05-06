using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public float maxHealth, health;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if(health <= 0)
        {
            Harvest();
        }
    }

    public void Damage(float value)
    {
        health -= value;
    }

    public void Harvest()
    {
        this.gameObject.SetActive(false);
    }
}
