using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public bool canPlace = true;

    int collisions;

    void Update()
    {
        MaterialController();

        if(collisions >= 1)
        {
            canPlace = false;

            return;
        }

        canPlace = true;
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
