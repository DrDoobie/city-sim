﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSRaycast : MonoBehaviour
{
    public Camera cam;
    public LayerMask layerMask;

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

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            
        }
    }
}