using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour {

	public bool buildMode = false;

	private void Update () {
		BuildingController();
	}

	private void BuildingController () {
		if(Input.GetButtonDown("Build Mode"))
		{
			buildMode = !buildMode;
		}
	}
}
