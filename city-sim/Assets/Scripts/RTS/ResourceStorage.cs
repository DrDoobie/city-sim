using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    public int storageValue;

    public void AddToStorage()
    {
        GameController.Instance.resourceController.maxWood += storageValue;
    }
}
