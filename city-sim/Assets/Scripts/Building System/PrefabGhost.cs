using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGhost : MonoBehaviour {

	public Material[] material;
	private Renderer rend;
	public bool placeable = true;

	private void Start () {
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		rend.sharedMaterial = material[0];
	}

	private void Update () {
		transform.parent.GetComponent<BuildingPrefab>().placeable = placeable;

		if(!placeable)
		{
			rend.sharedMaterial = material[1];

			return;
		}

		rend.sharedMaterial = material[0];
	}

	private void OnTriggerEnter (Collider other)
	{
		placeable = false;
	}

	private void OnTriggerExit (Collider other)
	{
		placeable = true;
	}
}
