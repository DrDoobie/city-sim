using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth, health;
    public GameObject body;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Debug.Log(transform.name + "Died!");
        GameController.Instance.resourceController.food += 5.0f;

        Destroy(body);
    }
}
