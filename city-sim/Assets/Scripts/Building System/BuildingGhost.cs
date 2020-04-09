using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingGhost : MonoBehaviour
{
    public int reqResources, resources;
    public GameObject building;

    bool inRange = false;
    
    private void Update()
    {
        if(inRange)
        {
            CheckResources();
        }

        if(GameController.Instance.rtsMode)
        {
            GameController.Instance.displayText.text = "";
        }
    }

    private void CheckResources()
    {
        GameController.Instance.displayText.text = "[Left Click] to add resources: " + resources.ToString("0#") + "/" + reqResources.ToString("0#");

        if(resources >= reqResources)
        {
            Build();
        }
    }

    private void Build()
    {
        Instantiate(building, transform.position, transform.rotation);

        GameController.Instance.displayText.text = "";

        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            inRange = false;

            GameController.Instance.displayText.text = "";
        }
    }
}
