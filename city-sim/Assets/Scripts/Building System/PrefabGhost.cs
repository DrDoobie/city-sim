using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGhost : MonoBehaviour {

	public Material[] material;
	private Transform selectedObj;
	private Renderer rend;
	private BuildingSystem buildingSystem;
	private GridSystem gridSystem;
	private BuildingPrefab buildingPrefab;
	[HideInInspector] public bool placeable = true;

	private void Start () {
		rend = GetComponent<Renderer>();
		rend.sharedMaterial = material[0];

		buildingSystem = FindObjectOfType<BuildingSystem>();
		gridSystem = FindObjectOfType<GridSystem>();
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

		gridSystem.selectedObj = selectedObj;
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
		selectedObj = other.transform;
	}

	private void OnTriggerExit (Collider other)
	{
		placeable = true;
		selectedObj = null;
	}
}
