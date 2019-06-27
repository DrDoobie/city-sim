using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public bool invertedMouseY = false;
    [SerializeField] private float mouseSens = 7.5f;
    private float xClamp = 0.0f;
	
    private void Update() {
        LookController();
    }

    private void LookController()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSens;

        mouseY = LimitCamera(mouseY);
        RotateMouseY(mouseY);
        transform.parent.Rotate(Vector3.up * mouseX);
    }

    private float LimitCamera (float mouseY) {
        xClamp += mouseY;

        if (xClamp > 90.0f)
        {
            xClamp = 90.0f;
            mouseY = 0.0f;
            ClampXRotation(270.0f);

        } else if (xClamp < -90.0f) {
            xClamp = -90.0f;
            mouseY = 0.0f;
            ClampXRotation(90.0f);
        }

        return mouseY;
    }

    private void RotateMouseY (float mouseY) {
        if(invertedMouseY)
        {
            transform.Rotate(-Vector3.left * mouseY);
            return;
        }

        transform.Rotate(Vector3.left * mouseY);
    }

    private void ClampXRotation(float value) {
        Vector3 eulerRot = transform.eulerAngles;
        eulerRot.x = value;
        transform.eulerAngles = eulerRot;
    }
}
