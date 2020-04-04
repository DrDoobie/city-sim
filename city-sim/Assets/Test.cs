using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject test;
    
    public void TestFunction()
    {
        Debug.Log("Boooom");

        test.GetComponent<PlayerCombat>().Attack();
    }
}
