using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public int maxOccupants = 1, occupants = 0;
    public GameObject occupant;

    void Start()
    {
        GameController.Instance.resourceController.homes++;
    }

    void Update()
    {
        if(occupants < maxOccupants)
        {
            SpawnOccupant();
        }
    }

    void SpawnOccupant()
    {
        Instantiate(occupant, occupant.transform.position, Quaternion.identity);

        occupants++;
    }
}
