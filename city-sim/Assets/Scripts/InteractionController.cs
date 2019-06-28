using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour {

    public GameObject selected;
	private GameController gameController;
	private BuildingSystem buildingSystem;

	private void Start () {
		gameController = FindObjectOfType<GameController>();
		buildingSystem = FindObjectOfType<BuildingSystem>();
	}

	private void Update ()
    {
        if(Input.GetButtonDown("Fire1") && (!buildingSystem.buildMode && gameController.omni))
		{
			Click();
		}

        if(selected != null)
        {
            Interactable interactable = selected.GetComponent<Interactable>();

            if(Input.GetButtonDown("Fire2"))
            {
                interactable.Harvest();
            }
        }
    }

    private void Click () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject.GetComponent<Interactable>())
            {
                selected = hit.transform.gameObject;

                return;
            }

            selected = null;
        }
    }
}
