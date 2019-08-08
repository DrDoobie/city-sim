using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabButton : MonoBehaviour {

	public int prefab;
	private GameController gameController;

	private void Awake () {
		gameController = FindObjectOfType<GameController>();
	}

	public void SelectPrefab () {
		gameController.buildingPrefab.prefab = prefab;
		gameController.prefabGhost.ResetGhost();

		Debug.Log("Selected " + gameController.prefabDatabase.prefab[prefab].prefabName + " prefab");
	}
}
