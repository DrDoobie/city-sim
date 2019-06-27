using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour {

	public bool buildMode = false;
	public GameObject uiGroup;
	private GameController gameController;

	private void Start () {
		gameController = FindObjectOfType<GameController>();
	}

	private void Update () {
		if(!gameController.omni)
		{
			buildMode = false;

			return;
		}

		BuildingController();
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
