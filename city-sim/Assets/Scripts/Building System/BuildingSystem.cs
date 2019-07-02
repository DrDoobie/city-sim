using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BuildingSystem : MonoBehaviour {

	public bool buildMode = false;
	public GameObject uiGroup;
	public Text infoText;
	public Color[] color;
	public Vector3 costTextOffset;
	private GameController gameController;

	private void Start () {
		gameController = FindObjectOfType<GameController>();
	}

	private void Update () {
		uiController();
		
		if(!gameController.isPaused)
		{
			if(!gameController.omni)
			{
				uiGroup.SetActive(false);
				buildMode = false;

				return;
			}

			BuildingController();
		}
	}

	private void uiController () {
		PrefabID objPrefab = gameController.prefabDatabase.prefab[gameController.buildingPrefab.prefab];

		if(gameController.buildingPrefab.canAfford)
		{
			infoText.color = color[0];

		} else {
			infoText.color = color[1];
		}

		infoText.gameObject.SetActive(true);
		infoText.text = "Prefab: " + gameController.prefabDatabase.prefab[gameController.buildingPrefab.prefab] + "\nCost: " + objPrefab.price + " " + objPrefab.objectType;

		if(gameController.isPaused)
		{
			return;
		}

		infoText.GetComponent<RectTransform>().position = (Input.mousePosition + costTextOffset);
	}

	private void BuildingController () {
		if(Input.GetButtonDown("Build Mode"))
		{
			buildMode = !buildMode;
		}

		if(!buildMode)
		{
			uiGroup.SetActive(false);

			return;
		}

		uiGroup.SetActive(true);
	}
}
