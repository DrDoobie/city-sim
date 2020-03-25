using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public int resources;
    public string resourcesString;
    public Text resourcesText;

    void Update ()
    {
        UIController();
    }

    private void UIController()
    {
        resourcesText.text = resourcesString + resources.ToString();
    }
}
