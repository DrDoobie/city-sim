using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPointer : MonoBehaviour
{
    public Camera cam;
    public GameObject ghostObj;

    void Update()
    {
        ghostObj.transform.position = SnapPosition(GetWorldPoint());
    }

    public Vector3 GetWorldPoint()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    public Vector3 SnapPosition(Vector3 original)
    {
        Vector3 snapped;

        snapped.x = Mathf.Floor(original.x + 0.5f);
        snapped.y = Mathf.Floor(original.y + 0.5f);
        snapped.z = Mathf.Floor(original.z + 0.5f);

        return snapped;
    }
}
