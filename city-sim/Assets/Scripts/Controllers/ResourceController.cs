﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public bool starving;
    public int stone, maxStone, wood, maxWood;
    public int population = 0, totalWorkers, avaliableWorkers, homes = 0;
    public float food, maxFood, foodLossRate;
    public Text stoneText, woodText, homesText, populationText, foodText;

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
