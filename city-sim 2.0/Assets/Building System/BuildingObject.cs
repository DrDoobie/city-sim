using UnityEngine;

[CreateAssetMenu(fileName = "BuildingObject", menuName = "Building System/Object")]
public class BuildingObject : ScriptableObject {
    [TextArea]
    public string objectInfo;
}