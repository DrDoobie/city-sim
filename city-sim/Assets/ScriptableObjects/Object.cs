using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Object", menuName = "Object")]
public class Object : ScriptableObject
{
    public bool layerSpecific, rotX, rotY, rotZ;
    public int cost;
    [TextArea] public string objInfo;
    public string objType;
    public string placementTag;
    public Sprite icon;
}
