using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSens = 100.0f;
    public Transform playerBod;
    private float xRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * (mouseSens * Time.deltaTime);
        float mouseY = Input.GetAxis("Mouse Y") * (mouseSens * Time.deltaTime);

        //Future reference, this is where to invert mouse y
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);
    
        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        playerBod.Rotate(Vector3.up * mouseX);
    }
}
