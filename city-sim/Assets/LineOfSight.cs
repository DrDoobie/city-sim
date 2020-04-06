using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public float viewRadius, viewAngle;

    public Vector3 DirFromAngle(float angleInDegrees)
    {
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0.0f ,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
