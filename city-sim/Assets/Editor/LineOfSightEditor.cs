using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LineOfSight))]
public class LineOfSightEditor : Editor
{
    void OnSceneGUI()
    {
        LineOfSight los = (LineOfSight)target;

        Handles.color = Color.white;

        Handles.DrawWireArc(los.transform.position, Vector3.up, Vector3.forward, 360.0f, los.viewRadius);

        Vector3 viewAngleA = los.DirFromAngle(-los.viewAngle/2.0f, false);
        Vector3 viewAngleB = los.DirFromAngle(los.viewAngle/2.0f, false);

        Handles.DrawLine(los.transform.position, los.transform.position + viewAngleA * los.viewRadius);
        Handles.DrawLine(los.transform.position, los.transform.position + viewAngleB * los.viewRadius);

        Handles.color = Color.red;
        foreach(Transform visibleTarget in los.visibleTargets)
        {
            Handles.DrawLine(los.transform.position, visibleTarget.position);
        }
    }
}
