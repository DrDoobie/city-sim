using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGhost : MonoBehaviour {

	public Material[] material;
	private Renderer rend;
	private BuildingSystem buildingSystem;
	private BuildingPrefab buildingPrefab;
	private bool placeable = true;

	private void Start () {
		rend = GetComponent<Renderer>();
		rend.sharedMaterial = material[0];

		buildingSystem = FindObjectOfType<BuildingSystem>();
		buildingPrefab = transform.parent.GetComponent<BuildingPrefab>();
	}

	private void Update () {
        Controller();
    }

    private void Controller() {
		if(!buildingSystem.buildMode)
		{
			rend.enabled = false;
			
		} else {
			rend.enabled = true;
		}

        buildingPrefab.placeable = placeable;

        if (!placeable)
        {
            rend.sharedMaterial = material[1];

            return;
        }

        rend.sharedMaterial = material[0];
    }

    private void OnTriggerEnter (Collider other)
	{
		placeable = false;
	}

	private void OnTriggerExit (Collider other)
	{
		placeable = true;
	}
}
