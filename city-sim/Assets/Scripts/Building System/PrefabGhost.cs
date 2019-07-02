using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGhost : MonoBehaviour {

	public Material[] material;
	private Transform selectedObj;
	private Renderer rend;
	private GameController gameController;
	[HideInInspector] public bool placeable = true;

	private void Start () {
		rend = GetComponent<Renderer>();
		rend.sharedMaterial = material[0];

		gameController = FindObjectOfType<GameController>();
	}

	private void Update () {
        Controller();
    }

    private void Controller() {
		if(!gameController.buildingSystem.buildMode)
		{
			rend.enabled = false;
			
		} else {
			rend.enabled = true;
		}

		gameController.gridSystem.selectedObj = selectedObj;
        gameController.buildingPrefab.placeable = placeable;

        if(!placeable)
        {


            rend.sharedMaterial = material[1];

            return;
        }

        rend.sharedMaterial = material[0];
    }

    private void OnTriggerStay (Collider other)
	{
		if(other == null)
		{
			placeable = true;
			
			return;
		}

		placeable = false;
		selectedObj = other.transform;
	}

	private void OnTriggerExit (Collider other)
	{
		placeable = true;
		selectedObj = null;
	}
}
