using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public bool starving;
    public int resources;
    public float reqFood;
    public Text resourcesText, foodText;

    float food;

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

        food -= Time.deltaTime;
    }

    private void UIController()
    {
        resourcesText.text = "Resources: " + resources.ToString();
        foodText.text = "Food: " + food.ToString("#") + "/" + reqFood;
    }
}
