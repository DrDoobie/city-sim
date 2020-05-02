using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSens = 100.0f, clampMin = -60.0f, clampMax = 75.0f;
    public Transform playerBod;

    float xRot = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens;//* Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        //Horizontal look
        playerBod.Rotate(Vector3.up * mouseX);

        //Vertical look
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, clampMin, clampMax);

        transform.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);
    }
}