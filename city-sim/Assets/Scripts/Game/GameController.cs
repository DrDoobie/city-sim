using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public bool omni = true, isPaused;
	public float pauseScale;
	public GameObject pauseMenu;
	private Camera mainCam, characterCam;
	private BuildingSystem buildingSystem;

	private void Start () {
		mainCam = Camera.main;
		characterCam = GameObject.FindWithTag("CharacterCamera").GetComponent<Camera>();
		buildingSystem = FindObjectOfType<BuildingSystem>();
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
