using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmPlot : MonoBehaviour
{
    public float soilMoisture;
    public GameObject plant;
    public List<Transform> seedSpots = new List<Transform>();

    void Start()
    {
        foreach(Transform child in transform)
        {
            seedSpots.Add(child);
        }
    }

    void Update()
    {
        GetComponent<Renderer>().material.SetFloat("_Glossiness", soilMoisture);
    }

    public void PlantSeed()
    {
        if(GameController.Instance.resourceController.food > 0)
        {
            PlayerCombat playerCombat = FindObjectOfType<PlayerCombat>();

            if(playerCombat.itemInHand != null && playerCombat.itemInHand.GetComponent<UseableItem>().item.itemType == "farming tool")
            {
                foreach(Transform spot in seedSpots)
                {
                    if(spot.childCount == 0)
                    {
                        GameObject seed = Instantiate(plant, spot.position, Quaternion.identity);

                        seed.transform.parent = spot;

                        GameController.Instance.resourceController.food--;

                        FindObjectOfType<AudioManager>().PlaySound("Plant Seed");

                        return;
                    }
                }
            }
        }
    }
}
