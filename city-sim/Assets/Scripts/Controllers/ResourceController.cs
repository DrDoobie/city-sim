using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public int resources;
    public string text;
    public Text resourcesText;

    void Update ()
    {
        UIController();
    }

    private void UIController()
    {
        resourcesText.text = text + resources.ToString();
    }
}
