﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f, sprintSpeed = 8.0f, gravity = -9.81f, groundDistance = 0.4f, jumpHeight = 1.0f, crouchHeight = 1.0f;
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    [SerializeField] bool isGrounded, isCrouching;
    float _movementSpeed, originalHeight;
    Vector3 velocity;

    void Start()
    {
        _movementSpeed = movementSpeed;

        originalHeight = controller.height;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.Instance.rtsMode)
        {
            MovementController();

            GetComponent<NavMeshAgent>().enabled = false;

            return;
        }

        GetComponent<NavMeshAgent>().enabled = true;
    }

    private void MovementController()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);

        //Movement
        if(!GameController.Instance.rtsMode)
        {
            controller.Move(move * movementSpeed * Time.deltaTime);
        }

        //Sprinting
        if(Input.GetButton("Sprint"))
        {
            movementSpeed = sprintSpeed;

        } else {
            movementSpeed = _movementSpeed;
        }

        //Crouching
        if(Input.GetButton("Crouch"))
        {
            controller.height = crouchHeight;

        } else {
            controller.height = originalHeight;
        }

        //Jumping
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
