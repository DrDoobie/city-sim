using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObject : MonoBehaviour
{
    public GameObject prefab;

    bool canPlace = true;
    [HideInInspector] public int collisions;

    void Update()
    {
        if(collisions >= 1)
        {
            canPlace = false;

        } else {
            canPlace = true;
        }
        
        GameController.Instance.rtsBuildingSystem.canPlace = canPlace;

        MaterialController();
    }

    void MaterialController()
    {
        MeshRenderer rend = GetComponent<MeshRenderer>();

        if(canPlace)
        {
            rend.material = GameController.Instance.rtsBuildingSystem.ghostMaterials[0];

        } else {
            rend.material = GameController.Instance.rtsBuildingSystem.ghostMaterials[1];
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 8)
        {
            Debug.Log("Colliding with " + other.name);

            collisions++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer != 8)
        {
            collisions--;
        }
    }
}
