using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSens = 5.0f;
    public Transform playerBod;
    private float xRotation = 0.0f;

    void Update()
    {
        if(!GameController.Instance.rtsMode)
        {
            LookController();
        }
    }

    private void LookController()
    {
        float mouseX = (Input.GetAxis("Mouse X") * mouseSens);
        float mouseY = (Input.GetAxis("Mouse Y") * mouseSens);

        //Future reference, this is where to invert mouse y
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -89.0f, 89.0f);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        playerBod.Rotate(Vector3.up * mouseX);
    }
}
