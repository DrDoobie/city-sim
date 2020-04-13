using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lighting Preset", menuName = "Lighting Preset")]
public class LightingPreset : ScriptableObject
{
    public Gradient ambientColor, directionalColor;
    //public Gradient fogColor;
}