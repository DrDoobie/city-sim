using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public CharacterController controller;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);

        controller.Move(move * movementSpeed * Time.deltaTime);
    }
}
