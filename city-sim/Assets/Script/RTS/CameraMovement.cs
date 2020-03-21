using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed = 10.0f, panBorderThickness = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementController();
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

        transform.position = pos;
    }
}
