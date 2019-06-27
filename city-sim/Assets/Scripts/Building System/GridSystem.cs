using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour {

	public float gridSize;
	public Transform target;
	public Transform prefab;
	private BuildingSystem buildingSystem;
	Vector3 truePos;

	private void Start () {
		buildingSystem = FindObjectOfType<BuildingSystem>();
	}
 
	private void LateUpdate () {
        if(buildingSystem.buildMode)
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
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(prefab, prefab.position, prefab.rotation);
        }
    }
}
