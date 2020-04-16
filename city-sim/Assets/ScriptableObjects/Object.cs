using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Object", menuName = "Object")]
public class Object : ScriptableObject
{
    public bool layerSpecific;
    public int cost;
    public string placementTag;
    public string objType;
    [TextArea] public string objInfo;
    public bool rotX, rotY, rotZ;
}
