using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public bool starving;
    public int resources;
    public int population = 0, totalWorkers, avaliableWorkers, homes = 0;
    public float food, maxFood, foodLossRate;
    public Text resourcesText, homesText, populationText, foodText;

    void Start()
    {
        food = maxFood;
    }

    void Update ()
    {
        UIController();
        FoodController();
    }

    void UIController()
    {
        resourcesText.text = "Resources: " + resources.ToString("0#");
        homesText.text = "Homes: " + homes.ToString("0#");
        populationText.text = "Population: " + population.ToString("0#");
        foodText.text = "Food: " + food.ToString("0#") + "/" + maxFood.ToString("0#");
    }

    void FoodController()
    {
        if(food >= maxFood)
        {
            food = maxFood;
        }

        if(food <= 0)
        {
            starving = true;

            return;
        }

        starving = false;

        food -= Time.deltaTime * (foodLossRate * population * 0.1f);
    }

    public void AddToPopulation()
    {
        population++;

        totalWorkers++;

        avaliableWorkers++;
    }
}
