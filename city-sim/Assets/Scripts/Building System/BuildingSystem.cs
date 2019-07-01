using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BuildingSystem : MonoBehaviour {

	public bool buildMode = false;
	public GameObject uiGroup;
	public Text costText;
	public Color[] color;
	public Vector3 costTextOffset;
	private GameController gameController;
	private BuildingPrefab buildingPrefab;

	private void Start () {
		gameController = FindObjectOfType<GameController>();
		buildingPrefab = FindObjectOfType<BuildingPrefab>();
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
		if(buildingPrefab.canAfford)
		{
			costText.color = color[0];

		} else {
			costText.color = color[1];
		}

		costText.gameObject.SetActive(true);
		costText.text = "Cost: " + buildingPrefab.prefab.GetComponent<ObjectPrefab>().price + " " + buildingPrefab.prefab.GetComponent<ObjectPrefab>().objectType;

		if(gameController.isPaused)
		{
			return;
		}

		costText.GetComponent<RectTransform>().position = (Input.mousePosition + costTextOffset);
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
