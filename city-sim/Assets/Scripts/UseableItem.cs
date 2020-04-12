using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableItem : MonoBehaviour
{
    public bool pickedUp;
    public Item item;

    void Update()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        if(pickedUp)
        {
            if(rigidBody)
            {
                rigidBody.isKinematic = true;
            }

            if(boxCollider)
            {
                boxCollider.enabled = false;
            }

            return;
        }

        boxCollider.enabled = true;

        rigidBody.isKinematic = false;
    }
}
