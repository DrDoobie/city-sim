using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour
{
    public float movementSpeed = 5.0f, sprintSpeed = 8.0f, gravity = -9.81f, groundDistance = 0.4f, jumpHeight = 1.0f;
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    
    [Header("Animation")]
    public bool isAnimated;
    public Animator animator;

    bool isGrounded, isCrouching;
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
        MovementController();
    }

    private void MovementController()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movePos = (transform.right * x) + (transform.forward * z);

        //Movement
        controller.Move(movePos * movementSpeed * Time.deltaTime);

        //Sprinting
        if(Input.GetButton("Sprint"))
        {
            movementSpeed = sprintSpeed;

        } else {
            movementSpeed = _movementSpeed;
        }

        //Jumping
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            //Jump();
        }

        if(isAnimated)
        {
            animator.SetBool("isWalking", movePos.magnitude > 0.1f ? true : false);

            animator.SetBool("isIdle", movePos.magnitude == 0.0f ? true : false);  
        }

        //Gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}