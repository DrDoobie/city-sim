using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingSystem : MonoBehaviour {

	public bool buildMode = false;
	public GameObject uiGroup;
	public Text costText;
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
		if(gameController.isPaused)
		{
			costText.gameObject.SetActive(false);

			return;
		}

		costText.GetComponent<RectTransform>().localPosition = (Input.mousePosition - (costTextOffset * 100));

		costText.gameObject.SetActive(true);
		costText.text = "Cost: " + buildingPrefab.prefab.GetComponent<ObjectPrefab>().price;
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
