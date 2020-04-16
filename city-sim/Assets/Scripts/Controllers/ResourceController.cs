using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public bool starving;

    [Header("UI")]
    public Text stoneText;
    public Text woodText, homesText, populationText, foodText;

    [Header("Town")]
    public int population = 0;
    public int totalWorkers, avaliableWorkers, homes = 0;

    [Header("Resources")]
    public int stone;
    public int maxStone, wood, maxWood;
    public float food, maxFood, foodLossRate;

    void Start()
    {
        food = maxFood;
    }

    void Update ()
    {
        UIController();
        ResourceCap();
        FoodController();
    }

    void UIController()
    {
        stoneText.text = "Stone: " + stone.ToString("0#")  + "/" + maxStone.ToString("0#");
        woodText.text = "Wood: " + wood.ToString("0#")  + "/" + maxWood.ToString("0#");
        foodText.text = "Food: " + food.ToString("0#") + "/" + maxFood.ToString("0#");

        homesText.text = "Homes: " + homes.ToString("0#");
        populationText.text = "Population: " + population.ToString("0#");
    }

    void ResourceCap()
    {
        if(food >= maxFood)
            food = maxFood;

        if(wood >= maxWood)
            wood = maxWood;

        if(stone >= maxStone)
            stone = maxStone;
    }

    void FoodController()
    {
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
