﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed = 10.0f, panBorderThickness = 10.0f, scrollSpeed = 5.0f, minY = 1.0f, maxY = 20.0f;
    public Vector2 panLimit;
    
    // Update is called once per frame
    void Update()
    {
        if(GameController.Instance.rtsMode)
        {
            MovementController();
        }
    }

    private void MovementController()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= (Screen.height - panBorderThickness))
        {
            pos.z += (panSpeed * Time.deltaTime);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= (panSpeed * Time.deltaTime);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= (panSpeed * Time.deltaTime);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= (Screen.width - panBorderThickness))
        {
            pos.x += (panSpeed * Time.deltaTime);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * (scrollSpeed * 100.0f) * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;  
    }
}
