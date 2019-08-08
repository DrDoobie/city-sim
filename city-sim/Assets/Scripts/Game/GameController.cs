using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public bool omni = true, isPaused;
	public float pauseScale;
	public GameObject pauseMenu;
	public Camera mainCam, characterCam;
	[HideInInspector] public Stats stats;
	[HideInInspector] public PrefabDatabase prefabDatabase;
	[HideInInspector] public InteractionController interactionController;
	[HideInInspector] public GridSystem gridSystem;
	[HideInInspector] public BuildingSystem buildingSystem;
	[HideInInspector] public BuildingPrefab buildingPrefab;
	[HideInInspector] public PrefabGhost prefabGhost;
	[HideInInspector] public GameObject player;

	private void Start () {
		mainCam = Camera.main;

		stats = FindObjectOfType<Stats>();
		Debug.Log("Successfully located " + stats);

		prefabDatabase = FindObjectOfType<PrefabDatabase>();
		Debug.Log("Successfully located " + prefabDatabase);

		interactionController = FindObjectOfType<InteractionController>();
		Debug.Log("Successfully located " + interactionController);

		gridSystem = FindObjectOfType<GridSystem>();
		Debug.Log("Successfully located " + gridSystem);

		buildingSystem = FindObjectOfType<BuildingSystem>();
		Debug.Log("Successfully located " + buildingSystem);

		buildingPrefab = FindObjectOfType<BuildingPrefab>();
		Debug.Log("Successfully located " + buildingPrefab);

		prefabGhost = FindObjectOfType<PrefabGhost>();
		Debug.Log("Successfully located " + prefabGhost);

		player = GameObject.FindGameObjectWithTag("Player");
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

		} else if(!characterCam.gameObject.activeInHierarchy) {
			Debug.Log("Erro: no character camera found");

			return;
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

	public void Pause () {
		isPaused = !isPaused;
	}

	public void Quit () {
		Application.Quit();
		Debug.Log("Quitting Game...");
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
