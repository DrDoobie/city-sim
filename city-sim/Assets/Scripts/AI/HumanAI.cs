using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAI : MonoBehaviour
{
    void Start()
    {
        GameController.Instance.resourceController.population++;
    }
}
