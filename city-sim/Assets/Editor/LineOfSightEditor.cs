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
    }
}
