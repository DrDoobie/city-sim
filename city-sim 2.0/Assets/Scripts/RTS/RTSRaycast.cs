using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSRaycast : MonoBehaviour
{
    //public LayerMask layerMask;
    //public GameObject selectedObject;

    Camera cam;

    void Start()
    {
        cam = GetComponent<RTSCamera>().rtsCam;
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Cast();
        }
    }

    void Cast()
    {
        RaycastHit hit;
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Interactions with flag object
            var flag = hit.transform.GetComponent<Flag>();

            if(flag)
            {
                flag.UseFlag();
            }

            /*var resource = hit.transform.GetComponent<Resource>();

            if(resource)
            {
                selectedObject = hit.transform.gameObject;
            }*/
        }
    }
}
