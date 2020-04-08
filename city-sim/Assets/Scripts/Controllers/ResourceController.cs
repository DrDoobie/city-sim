using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public bool starving;
    public int resources, homes = 0;
    public float food, maxFood, foodLossRate;
    public Text resourcesText, homesText, populationText, foodText;
    public List<GameObject> population = new List<GameObject>();
    
    void Start()
    {
        food = maxFood;
    }

    void Update ()
    {
        UIController();
        FoodController();
    }

    private void FoodController()
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

        food -= Time.deltaTime * (foodLossRate * population.Capacity * 0.1f);
    }

    private void UIController()
    {
        resourcesText.text = "Resources: " + resources.ToString("0#");
        homesText.text = "Homes: " + homes.ToString("0#");
        populationText.text = "Population: " + population.Capacity.ToString("0#");
        foodText.text = "Food: " + food.ToString("0#") + "/" + maxFood.ToString("0#");
    }
}
