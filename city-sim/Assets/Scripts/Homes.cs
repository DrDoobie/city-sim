using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homes : MonoBehaviour
{
    public int maxOccupants = 1, currentOccupants = 0;
    public GameObject occupantObj;

    void Start()
    {
        GameController.Instance.resourceController.homes++;

        if(currentOccupants < maxOccupants)
        {
            SpawnOccupant();
        }
    }

    void SpawnOccupant()
    {
        Instantiate(occupantObj, transform.position, occupantObj.transform.rotation);

        currentOccupants ++;
    }
}
