using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int value;
    public float maxHealth, health;
    public GameObject body;

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
        GameController.Instance.resourceController.resources += value;

        this.gameObject.SetActive(false);
    }

    public void MineResource(float val)
    {
        health -= val;
    }
}
