using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RTSControls : MonoBehaviour
{
    public Camera cam;

    void Update()
    {
        Controller();
    }

    void Controller()
    {
        Vector3 mousePos = Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Input.GetButtonDown("Fire1"))
        {
            if(Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.point);

                GameController.Instance.playerAgent.destination = hit.point;
            }
        }
    }
}
