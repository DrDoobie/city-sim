﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour {

    public GameObject selectionRing, selected;
	private GameController gameController;
	private BuildingSystem buildingSystem;

	private void Start () {
		gameController = FindObjectOfType<GameController>();
		buildingSystem = FindObjectOfType<BuildingSystem>();
	}

	private void Update ()
    {
        SelectionController();

        if(Input.GetButtonDown("Fire1") && (!buildingSystem.buildMode && gameController.omni))
		{
			Click();
		}
    }

    private void SelectionController () {
        if(selected == null)
        {
            selectionRing.SetActive(false);

            return;
        }

        selectionRing.transform.position = selected.transform.position;
        selectionRing.SetActive(true);
    }

    private void Click () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject.GetComponent<Interactable>())
            {
                if(hit.transform.gameObject == selected)
                {
                    Interactable interactable = selected.GetComponent<Interactable>();
                    interactable.Harvest();

                    return;
                }

                selected = hit.transform.gameObject;

                return;
            }

            selected = null;
        }
    }
}
