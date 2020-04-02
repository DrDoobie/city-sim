using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObject : MonoBehaviour
{
    public bool canPlace = true;
    
    string reqTag = "Terrain";

    void Awake()
    {
        GetComponent<Renderer>().material = GameController.Instance.buildingSystem.ghostMaterial[0];

        gameObject.layer = 2;

        //Checking for and removing components
        if(GetComponent<ResourceGenerator>())
        {
            Destroy(GetComponent<ResourceGenerator>());
        }

        if(GetComponent<MeshCollider>())
        {
            GetComponent<MeshCollider>().convex = true;
            GetComponent<MeshCollider>().isTrigger = true;
        }
        
        if(GetComponent<BoxCollider>())
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }

        //Adding components
        gameObject.AddComponent(typeof(Rigidbody));

        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
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

    void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag(reqTag))
        {
            canPlace = false;

            //Debug.Log("Colliding with " + other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        canPlace = true;
    }
}
