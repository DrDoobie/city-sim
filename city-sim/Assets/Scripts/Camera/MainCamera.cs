using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public float panSpeed = 10.0f, speedMultiplier = 1.5f;
	public float panLimit = 10.0f;
	public float minFov = 10.0f;
	public float maxFov = 90.0f;
	public float fovSensitivity = 20.0f;
	private Camera cam;
    private GameController gameController;

	private void Start () {
		cam = GetComponent<Camera>();
        gameController = FindObjectOfType<GameController>();
	}

	private void Update () {
        if(gameController.omni)
        {
            CameraMovement();
            CameraZoom();
        }
    }

    private void CameraMovement () {
		float speed;

		if(Input.GetButton("Sprint"))
		{
			speed = (panSpeed * speedMultiplier);

		} else {
			speed = panSpeed;
		}

        //Some ugly ass code here, need to fix asap but I'm too high
        Vector3 pos = transform.position;

        if(Input.GetButton("Up"))
        {
            pos.z += (speed * Time.deltaTime);
        }

        if(Input.GetButton("Down"))
        {
            pos.z -= (speed * Time.deltaTime);
        }

        if(Input.GetButton("Left"))
        {
            pos.x -= (speed * Time.deltaTime);
        }

        if(Input.GetButton("Right"))
        {
            pos.x += (speed * Time.deltaTime);
        }

        if(!gameController.buildingSystem.buildMode)
        {
            if(Input.mousePosition.y >= (Screen.height - panLimit))
            {
            pos.z += (speed * Time.deltaTime);
            }

            if(Input.mousePosition.y <= panLimit)
            {
                pos.z -= (speed * Time.deltaTime);
            }

            if(Input.mousePosition.x <= panLimit)
            {
                pos.x -= (speed * Time.deltaTime);
            }

            if(Input.mousePosition.x >= (Screen.width - panLimit))
            {
                pos.x += (speed * Time.deltaTime);
            }
        }

        transform.position = pos;
    }
    
    private void CameraZoom() {
        float fov = cam.fieldOfView;

        fov -= (Input.GetAxis("Mouse ScrollWheel") * fovSensitivity);
        fov = Mathf.Clamp(fov, minFov, maxFov);

        cam.fieldOfView = fov;
    }
} 