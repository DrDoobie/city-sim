using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int value;
    public float maxHealth, health;
    public string resourceType;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        HealthController();
    }

    void HealthController()
    {
        if(health <= 0)
        {
            Harvest();
        }
    }

    void Harvest()
    {
        if(resourceType == "Wood")
            GameController.Instance.resourceController.wood += value;

        if(resourceType == "Stone")
            GameController.Instance.resourceController.stone += value;
            
        this.gameObject.SetActive(false);
    }

    public void MineResource(float val)
    {
        health -= val;
    }
}
