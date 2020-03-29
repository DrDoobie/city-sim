using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Creature", menuName = "Creature")]
public class Creature : ScriptableObject
{
    [TextArea] public string info;
    public float wanderTime, growTime, birthCoolDown;
    public GameObject baby;
}
