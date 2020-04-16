using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    public int storageValue;
    public string resourceType;

    public void AddToStorage()
    {
        if(resourceType == "Wood")
            GameController.Instance.resourceController.maxWood += storageValue;
          
        if(resourceType == "Stone")
            GameController.Instance.resourceController.maxStone += storageValue;
    }
}
