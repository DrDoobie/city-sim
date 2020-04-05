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
        GameController.Instance.resourceController.population++;
        
        health = maxHealth;
        hunger = maxHunger;
    }

    void Update ()
    {
        UIController();
        HungerController();
        HealthController();
    }

    private void HungerController()
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

    private void HealthController()
    {
        if(health <= 0.0f)
        {
            Debug.Log("Died!");

            Time.timeScale = 0.0f;
        }
    }

    public void TakeDamage(float value)
    {
        health -= value;

        FindObjectOfType<AudioManager>().PlaySound("Damage");
    }

    private void UIController()
    {
        hungerBar.maxValue = maxHunger;
        hungerBar.value = hunger;

        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }
}
