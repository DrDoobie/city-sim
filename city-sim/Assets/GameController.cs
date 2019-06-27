﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public bool omni = true;
	private Camera mainCam, characterCam;

	private void Start () {
		mainCam = Camera.main;
		characterCam = GameObject.FindWithTag("CharacterCamera").GetComponent<Camera>();
	}

	private void Update () {
        CameraController();
    }

    private void CameraController() {
		if(Input.GetButtonDown("Camera Switch"))
		{
			omni = !omni;
		}

		if(!omni)
		{
			mainCam.enabled = false;
			characterCam.enabled = true;

			return;
		}

		characterCam.enabled = false;
        mainCam.enabled = true;
    }
}
