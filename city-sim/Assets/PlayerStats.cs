using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth, maxHunger;
    public Slider hungerBar;

    float health, hunger;

    void Start () {
        health = maxHealth;
        hunger = maxHunger;
    }

    void Update ()
    {
        UIController();
        Hunger();
    }

    private void Hunger()
    {
        if(GameController.Instance.resourceController.starving)
        {
            if(hunger <= 0)
            {
                return;
            }

            hunger -= Time.deltaTime;
        }
    }

    private void UIController()
    {
        hungerBar.maxValue = maxHunger;
        hungerBar.value = hunger;
    }
}
