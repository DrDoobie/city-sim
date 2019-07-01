using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public bool omni = true, isPaused;
	public float pauseScale;
	public GameObject pauseMenu;
	private Camera mainCam, characterCam;
	[HideInInspector] public Stats stats;
	[HideInInspector] public InteractionController interactionController;
	[HideInInspector] public GridSystem gridSystem;
	[HideInInspector] public BuildingSystem buildingSystem;
	[HideInInspector] public BuildingPrefab buildingPrefab;
	[HideInInspector] public PrefabGhost prefabGhost;

	private void Start () {
		mainCam = Camera.main;
		characterCam = GameObject.FindWithTag("CharacterCamera").GetComponent<Camera>();

		stats = FindObjectOfType<Stats>();
		interactionController = FindObjectOfType<InteractionController>();
		gridSystem = FindObjectOfType<GridSystem>();
		buildingSystem = FindObjectOfType<BuildingSystem>();
		buildingPrefab = FindObjectOfType<BuildingPrefab>();
		prefabGhost = FindObjectOfType<PrefabGhost>();
	}

	private void Update () {
		PauseController();
        CameraController();
    }

	private void PauseController () {
		if(Input.GetButtonDown("Pause"))
		{
			isPaused = !isPaused;
		}

		if(isPaused)
		{
			if(!buildingSystem.buildMode)
			{
				pauseMenu.SetActive(true);
			}

			Time.timeScale = pauseScale;

			return;
		}

		pauseMenu.SetActive(false);
		Time.timeScale = 1.0f;
	}

    private void CameraController () {
		if(Input.GetButtonDown("Camera Switch"))
		{
			omni = !omni;
		}

		if(!omni)
		{
			mainCam.enabled = false;
			characterCam.enabled = true;
			LockCursor();

			return;
		}

		characterCam.enabled = false;
        mainCam.enabled = true;
		UnlockCursor();
    }

	public void LockCursor () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void UnlockCursor () {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
