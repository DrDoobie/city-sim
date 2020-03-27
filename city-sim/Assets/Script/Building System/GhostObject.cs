using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObject : MonoBehaviour
{
    public bool canPlace = true;

    void Awake()
    {
        GetComponent<Renderer>().material = GameController.Instance.buildingSystem.ghostMaterial[0];

        gameObject.layer = 2;

        if(GetComponent<MeshCollider>())
        {
            GetComponent<MeshCollider>().convex = true;
            GetComponent<MeshCollider>().isTrigger = true;
        }
    }

    void Update()
    {
        if(canPlace)
        {
            GetComponent<Renderer>().material = GameController.Instance.buildingSystem.ghostMaterial[0];

            return;
        }

        GetComponent<Renderer>().material = GameController.Instance.buildingSystem.ghostMaterial[1];
    }

    void OnTriggerEnter(Collider other)
    {
        canPlace = false;
    }

    void OnTriggerExit(Collider other)
    {
        canPlace = true;
    }
}
