using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabButton : MonoBehaviour {

	public GameObject prefab;
	private BuildingPrefab buildingPrefab;

	private void Awake () {
		buildingPrefab = FindObjectOfType<BuildingPrefab>();
	}

	public void SelectPrefab () {
		buildingPrefab.prefab = prefab;
	}
}
