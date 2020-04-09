using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth, maxHunger, health;
    public Slider healthBar, hungerBar;

    float hunger;

    void Start () {
        //GameController.Instance.resourceController.population++;
        
        health = maxHealth;
        hunger = maxHunger;
    }

    void Update ()
    {
        UIController();
        HungerController();
        HealthController();
    }

    void UIController()
    {
        hungerBar.maxValue = maxHunger;
        hungerBar.value = hunger;

        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    void HungerController()
    {
        if(GameController.Instance.resourceController.starving)
        {
            if(hunger <= 0.0f)
            {
                health -= Time.deltaTime;

                return;
            }

            hunger -= Time.deltaTime;
        }
    }

    void HealthController()
    {
        if(health <= 0.0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player death");

        GameController.Instance.player.position = new Vector3(0.0f, 0.0f, 0.0f);

        GameController.Instance.rtsMode = true;

        hunger = maxHunger;
        
        health = maxHealth;
    }

    public void TakeDamage(float value)
    {
        health -= value;

        FindObjectOfType<AudioManager>().PlaySound("Damage");
    }
}
