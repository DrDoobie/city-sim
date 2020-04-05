using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Object")]
public class Object : ScriptableObject
{
    public bool rotX, rotY, rotZ;
    [TextArea] public string objInfo;
    public int cost;
}
