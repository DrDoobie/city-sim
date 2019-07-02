using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGhost : MonoBehaviour {

	public Material[] material;
	public bool ghostSpawned;
	private Transform selectedObj;
	private Renderer rend;
	private GameController gameController;
	[HideInInspector] public bool placeable = true;

	private void Start () {
		gameController = FindObjectOfType<GameController>();
	}

	private void Update () {
		GhostController();
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
	
	private void GhostController () {
		Transform ghostTransform = gameController.prefabDatabase.prefab[gameController.buildingPrefab.prefab].obj.transform.GetChild(0);

		if(ghostSpawned)
		{
			return;
		}

		Transform ghost = Instantiate(ghostTransform);
		
		Destroy(ghost.GetComponent<BoxCollider>());
		Destroy(ghost.GetComponent<Rigidbody>());

		ghost.gameObject.layer = 2;
		ghost.transform.parent = this.transform;

		rend = ghost.GetComponent<Renderer>();
		rend.sharedMaterial = material[0];

		ghostSpawned = true;
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
