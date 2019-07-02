using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public bool wood, stone;
	public int amount;
	private GameController gameController;

	private void Awake () {
		gameController = FindObjectOfType<GameController>();
	}

	public void Harvest () {
		if(wood)
		{
			gameController.stats.wood += amount;
		}
		
		if(stone)
		{
			gameController.stats.stone += amount;
		}

		if(gameController.interactionController.selected != null)
		{
			gameController.interactionController.selected = null;
		}
		
		Destroy(this.gameObject);
	}
}
