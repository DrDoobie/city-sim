﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObject : MonoBehaviour
{
    public bool canPlace = true;
    
    string reqTag = "Terrain";

    void Awake()
    {
        gameObject.layer = 2;

        //Checking for and removing components

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

        Renderer rend = GetComponent<Renderer>();

        if(rend.materials.Length > 1)
        {
            //Debug.Log("More than 1 material active");
                
            Material[] ghostMats;

            ghostMats = rend.materials;

            ghostMats[0] = GameController.Instance.buildingSystem.ghostMaterial[0];
            ghostMats[1] = GameController.Instance.buildingSystem.ghostMaterial[0];

            rend.materials = ghostMats;

            return;
        }

        GetComponent<Renderer>().material = GameController.Instance.buildingSystem.ghostMaterial[0];
    }

    void Update()
    {
        //Material controller
        Renderer rend = GetComponent<Renderer>();

        if(canPlace)
        {
            if(rend.materials.Length > 1)
            {
                //Debug.Log("More than 1 material active");
                
                Material[] ghostMats;

                ghostMats = rend.materials;

                ghostMats[0] = GameController.Instance.buildingSystem.ghostMaterial[0];
                ghostMats[1] = GameController.Instance.buildingSystem.ghostMaterial[0];

                rend.materials = ghostMats;

                return;
            }

            rend.material = GameController.Instance.buildingSystem.ghostMaterial[0];

            return;
        }

        if(rend.materials.Length > 1)
        {
            //Debug.Log("More than 1 material active");
            
            Material[] ghostMats;

            ghostMats = rend.materials;

            ghostMats[0] = GameController.Instance.buildingSystem.ghostMaterial[1];
            ghostMats[1] = GameController.Instance.buildingSystem.ghostMaterial[1];

            rend.materials = ghostMats;

            return;
        }

        rend.material = GameController.Instance.buildingSystem.ghostMaterial[1];
    }

    void OnTriggerStay(Collider other)
    {
        //

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
