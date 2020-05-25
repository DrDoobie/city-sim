using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    public float normalSpeed = 1.0f, shiftSpeed = 5.0f, rotationSpeed = 0.1f;

    Vector2 p1, p2;

    [SerializeField] float moveSpeed;

    void Update()
    {    
        if(!GameController.Instance.playerUsingUI)
        {
            Controller();
        }

        GetCameraRotation();
    }

    void Controller()
    {
        if(Input.GetButton("Sprint"))
        {
            moveSpeed = shiftSpeed;

        } else {
            moveSpeed = normalSpeed;
        }

        //Get input
        float horspeed = transform.position.y * moveSpeed * Input.GetAxis("Horizontal");
        float vertspeed = transform.position.y * moveSpeed * Input.GetAxis("Vertical");

        //float scrollspeed = Mathf.Log(transform.position.y) * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        /*//Clamp
        if(transform.position.y >= maxHeight && scrollspeed > 0.0f)
        {
            scrollspeed = 0.0f;

        } else if(transform.position.y <= minHeight && scrollspeed < 0.0f) {
            scrollspeed = 0.0f;
        }*/

        //Vector3 verticalMove = new Vector3(0.0f, (scrollspeed * 100.0f), 0.0f);
        Vector3 lateralMove = horspeed * transform.right;
        Vector3 forwardMove = transform.forward;

        forwardMove.y = 0.0f;
        forwardMove.Normalize();
        forwardMove *= vertspeed;

        //Actual movement
        Vector3 move = (lateralMove + forwardMove) * Time.deltaTime;

        transform.position += move;
    }

    void GetCameraRotation ()
    {
        if(Input.GetMouseButtonDown(2))
        {
            p1 = Input.mousePosition;
        }

        if(Input.GetMouseButton(2))
        {
            p2 = Input.mousePosition;

            float dx = (p1 - p2).x * rotationSpeed;
            float dy = (p2 - p1).y * rotationSpeed;

            transform.rotation *= Quaternion.Euler(new Vector3(0.0f, -dx, 0.0f));//Rotate camera horizontal
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0.0f, 0.0f));//Rotate camera vertical

            p1 = p2;
        }
    }
}
