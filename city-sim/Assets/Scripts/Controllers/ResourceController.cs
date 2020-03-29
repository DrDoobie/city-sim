using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public bool starving;
    public int resources;
    public float food, reqFood, foodLossRate;
    public Text resourcesText, foodText;
    
    void Start()
    {
        food = reqFood;
    }

    void Update ()
    {
        UIController();

        if(food <= 0)
        {
            starving = true;

            return;
        }

        food -= Time.deltaTime * (foodLossRate * 0.1f);
    }

    private void UIController()
    {
        resourcesText.text = "Resources: " + resources.ToString("0#");
        foodText.text = "Food: " + food.ToString("0#") + "/" + reqFood.ToString("0#");
    }
}
