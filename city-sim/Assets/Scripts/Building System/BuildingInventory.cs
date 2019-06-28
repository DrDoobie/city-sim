using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInventory : MonoBehaviour {

	public bool isOpen;
	public GameObject uiGroup;

	private void Update () {
		if(Input.GetButtonDown("Inventory"))
		{
			isOpen = !isOpen;
		}

		if(!isOpen)
		{
			uiGroup.SetActive(false);

			return;
		}
		
		uiGroup.SetActive(true);
	}
}
