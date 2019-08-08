using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGhost : MonoBehaviour {

	public Material[] material;
	public bool ghostSpawned;
	private Transform selectedObj;
	public Renderer rend;
	private GameController gameController;
	[HideInInspector] public bool placeable = true;

	private void Start () {
		gameController = FindObjectOfType<GameController>();
	}

	private void Update () {
		GhostController();
        Controller();
    }

	private void GhostController () {
        Transform ghostTransform = gameController.prefabDatabase.prefab[gameController.buildingPrefab.prefab].obj.transform.GetChild(0);

        if(ghostSpawned)
        {
            return;
        }

		//Detect a change in selected prefab and spawn ghost accordingly

        SpawnGhost(ghostTransform);
    }

	private void Controller () {
		gameController.gridSystem.selectedObj = selectedObj;
        gameController.buildingPrefab.placeable = placeable;

        if(!placeable)
        {
            rend.sharedMaterial = material[1];

            return;
        }

        rend.sharedMaterial = material[0];
    }

	public void ResetGhost () {
		if(!ghostSpawned || rend == null)
		{
			return;
		}

		Destroy(rend.gameObject);
		ghostSpawned = false;
		rend = null;
	}

    private void SpawnGhost (Transform ghostTransform) {
        Transform ghost = Instantiate(ghostTransform, transform.position, transform.rotation);

        Destroy(ghost.GetComponent<BoxCollider>());
        Destroy(ghost.GetComponent<Rigidbody>());

		rend = ghost.GetComponent<Renderer>();

        ghost.gameObject.layer = 2;
        ghost.transform.parent = this.transform;
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
