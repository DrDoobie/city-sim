using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingGhost : MonoBehaviour
{
    public int reqResources, resources;
    public Material finishedMaterial;
    public Renderer rend;

    bool inRange = false;
    
    void Start()
    {
        Homes home = GetComponent<Homes>();

        WorkPlace workPlace = GetComponent<WorkPlace>();

        if(home)
            home.enabled = false;

        if(workPlace)
            workPlace.enabled = false;
    }

    void Update()
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

    void CheckResources()
    {
        GameController.Instance.displayText.text = "[Left Click] to add resources: " + resources.ToString("0#") + "/" + reqResources.ToString("0#");

        if(resources >= reqResources)
        {
            Build();
        }
    }

    void Build()
    {
        rend.material = finishedMaterial;

        GameController.Instance.displayText.text = "";

        Homes home = GetComponent<Homes>();

        WorkPlace workPlace = GetComponent<WorkPlace>();

        if(home)
            home.enabled = true;

        if(workPlace)
            workPlace.enabled = true;

        Destroy(this);
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
