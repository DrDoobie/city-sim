using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventReferencer : MonoBehaviour
{
    public GameObject referenceObject;

    public void Event()
    {
        referenceObject.GetComponent<PlayerCombat>().Attack();
    }
}
