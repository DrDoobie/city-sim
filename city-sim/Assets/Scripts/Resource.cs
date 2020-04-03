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

    private void HealthController()
    {
        if(health <= 0)
        {
            Harvest();
        }
    }

    private void Harvest()
    {
        GameController.Instance.resourceController.resources += value;
        
        Destroy(body);
    }
}
