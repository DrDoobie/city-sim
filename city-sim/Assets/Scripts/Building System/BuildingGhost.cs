using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingGhost : MonoBehaviour
{
    public int reqResources, resources;
    public string requiredResource;
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
        if(requiredResource == "Wood")
            GameController.Instance.displayText.text = "[Left Click] to add wood: " + resources.ToString("0#") + "/" + reqResources.ToString("0#");

        if(requiredResource == "Stone")
            GameController.Instance.displayText.text = "[Left Click] to add stone: " + resources.ToString("0#") + "/" + reqResources.ToString("0#");

        if(resources >= reqResources)
        {
            Build();
        }
    }

    void Build()
    {
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        Homes home = GetComponent<Homes>();
        WorkPlace workPlace = GetComponent<WorkPlace>();

        rend.material = finishedMaterial;

        GameController.Instance.displayText.text = "";

        if(meshCollider)
        {
            meshCollider.isTrigger = false;

            meshCollider.convex = false;    
        }

        if(home)
            home.enabled = true;

        if(workPlace)
            workPlace.enabled = true;

        gameObject.layer = 0;

        Destroy(this);
    }

    public void AddResources(int value)
    {
        if(inRange)
        {
            PlayerCombat playerCombat = FindObjectOfType<PlayerCombat>();

            if(playerCombat.itemInHand != null && playerCombat.itemInHand.GetComponent<UseableItem>().item.itemType == "building tool")
            {
                if(requiredResource == "Wood")
                {
                    resources += value;

                    GameController.Instance.resourceController.wood--;
                }

                if(requiredResource == "Stone")
                {
                    resources += value;
                    
                    GameController.Instance.resourceController.stone--;    
                }

                FindObjectOfType<AudioManager>().PlaySound("Build");
            }
        }
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
