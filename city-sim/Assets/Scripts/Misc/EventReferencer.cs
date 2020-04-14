using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventReferencer : MonoBehaviour
{
    public GameObject obj;
    public string functionType;
    
    public void ReferenceFunction()
    {
        if(functionType == "PlayerAttack")
        {
            obj.GetComponent<PlayerCombat>().Attack();
        }

        if(functionType == "HumanAttack")
        {
            obj.GetComponent<Human>().Attack();
        }

        if(functionType == "AnimalAttack")
        {
            obj.GetComponent<Animal>().Attack();
        }
    }
}
