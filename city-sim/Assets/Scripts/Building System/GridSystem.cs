﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour {

	public float gridSize;
	public Transform target;
	public Transform prefab;
    private GameController gameController;
	private BuildingSystem buildingSystem;
    private BuildingPrefab buildingPrefab;
	Vector3 truePos;

	private void Start () {
        gameController = FindObjectOfType<GameController>();
		buildingSystem = FindObjectOfType<BuildingSystem>();
        buildingPrefab = prefab.GetComponent<BuildingPrefab>();
	}
 
	private void LateUpdate () {
        if(buildingSystem.buildMode && gameController.omni)
		{
            TargetController();
			GridController();
            PlacementController();
		}
    }

    private void TargetController () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 9))
        {
            target.position = hit.point;

			Vector3 temp = target.position;
			temp.y = 0.0f;

			target.position = temp;
        }
    }

    private void GridController () {
        truePos.x = (Mathf.Floor(target.position.x / gridSize) * gridSize);
        truePos.y = (Mathf.Floor(target.position.y / gridSize) * gridSize);
        truePos.z = (Mathf.Floor(target.position.z / gridSize) * gridSize);

        prefab.transform.position = truePos;
    }

    private void PlacementController () {
        if(Input.GetButtonDown("Fire1") && buildingPrefab.placeable)
        {
            Instantiate(buildingPrefab.prefab, prefab.position, prefab.rotation);
        }
    }
}
