using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public bool wood, stone;
	public int amount;
	private InteractionController interactionController;
	private Stats stats;

	private void Awake () {
		interactionController = FindObjectOfType<InteractionController>();
		stats = FindObjectOfType<Stats>();
	}

	public void Harvest () {
		if(wood)
		{
			stats.wood += amount;
		}
		
		if(stone)
		{
			stats.stone += amount;
		}

		if(interactionController.selected != null)
		{
			interactionController.selected = null;
		}

		Debug.Log("Harvested " + this.transform.name);
		Destroy(this.gameObject);
	}
}
