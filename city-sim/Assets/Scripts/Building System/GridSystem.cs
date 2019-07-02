using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour {

	public float gridSize = 0.5f, rotDegree = 30.0f;
	public Transform selectedObj, target, prefab;
    private GameController gameController;
    private PrefabID objPrefab;
	Vector3 truePos;

	private void Start () {
        gameController = FindObjectOfType<GameController>();
	}
 
	private void LateUpdate () {
        CostController();

        if(gameController.buildingSystem.buildMode && gameController.omni && !gameController.isPaused)
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

    private void CostController () {
        objPrefab = gameController.prefabDatabase.prefab[gameController.buildingPrefab.prefab];

        if(objPrefab.objectType == "wood" && (gameController.stats.wood >= objPrefab.price))
        {
            gameController.buildingPrefab.canAfford = true;
            
            return;
        }

        if(objPrefab.objectType == "stone" && (gameController.stats.stone >= objPrefab.price))
        {
            gameController.buildingPrefab.canAfford = true;

            return;
        }

        if(objPrefab.objectType == "money" && (gameController.stats.money >= objPrefab.price))
        {
            gameController.buildingPrefab.canAfford = true;

            return;
        }

        gameController.buildingPrefab.canAfford = false;
    }

    private void PlacementController () 
    {
        if(Input.GetButtonDown("Fire1") && (gameController.buildingPrefab.placeable && gameController.buildingPrefab.canAfford)) //Want to fix this dirty code, look into c# dictionary functions
        {
            if(objPrefab.objectType == "wood")
            {
                gameController.stats.wood -= objPrefab.price;
            }

            if(objPrefab.objectType == "stone")
            {
                gameController.stats.stone -= objPrefab.price;
            }

            if(objPrefab.objectType == "money")
            {
                gameController.stats.money -= objPrefab.price;
            }

            Instantiate(gameController.prefabDatabase.prefab[gameController.buildingPrefab.prefab].obj, prefab.position, prefab.rotation);
            Debug.Log("Success");

        } else if(Input.GetButtonDown("Fire1") && !gameController.buildingPrefab.placeable) {
            Debug.Log("Error: not enough space");

        } else if(Input.GetButtonDown("Fire1") && !gameController.buildingPrefab.canAfford) {
            Debug.Log("Error: can't afford");
        }

        if(Input.GetButtonDown("Rotate"))
        {
            prefab.transform.Rotate(0.0f, rotDegree, 0.0f);
        }

        if(Input.GetButtonDown("Delete") && selectedObj != null)
        {
            gameController.stats.money += 1.0f;
            Destroy(selectedObj.parent.gameObject);
            gameController.prefabGhost.placeable = true;
        }
    }
}
