using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventReferencer : MonoBehaviour
{
    public GameObject obj;
    
    public void ReferenceFunction()
    {
        obj.GetComponent<PlayerCombat2>().Attack();
    }
}
