using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public int resources, reqFood;
    public Text resourcesText, foodText;

    int food;

    void Start()
    {
        food = reqFood;
    }

    void Update ()
    {
        UIController();
    }

    private void UIController()
    {
        resourcesText.text = "Resources: " + resources.ToString();
        foodText.text = "Food: " + food.ToString() + "/" + reqFood;
    }
}
