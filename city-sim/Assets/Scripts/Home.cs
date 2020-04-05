using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    void Start()
    {
        GameController.Instance.resourceController.homes++;
    }
}
