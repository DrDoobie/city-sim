using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private bool isJumping;
    private GameController gameController;
    private CharacterController controller;
    [SerializeField] private float movementSpeed = 5.0f, jumpMultiplier = 2.5f, slopeForce = 2.0f;
    [SerializeField] private AnimationCurve jumpCurve;

    private bool OnSlope () {
       if(!isJumping)
       {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, (controller.height / 2) * 1.5f) && (hit.normal != Vector3.up))
            {
                return true;
            }
       }

        return false;
    }

    private float Jump (float airTime, float jumpForce) {
        controller.Move((Vector3.up * jumpForce * jumpMultiplier) * Time.deltaTime);
        airTime += Time.deltaTime;
        
        return airTime;
    }

	private void Awake () {
		controller = GetComponent<CharacterController>();
        gameController = FindObjectOfType<GameController>();
	}

    private void Update() {
        if(!gameController.omni)
        {
            MovementController();
        }
    }

    private void MovementController()
    {
        float vertInput = Input.GetAxis("Vertical");
        float horizInput = Input.GetAxis("Horizontal");
        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 sideMovement = transform.right * horizInput;

        MovePlayer(vertInput, horizInput, forwardMovement, sideMovement);
        InitiateJump();
    }

    private void MovePlayer (float vertInput, float horizInput, Vector3 forwardMovement, Vector3 sideMovement) {
        controller.SimpleMove(Vector3.ClampMagnitude(forwardMovement + sideMovement, 1.0f) * movementSpeed);

        if ((vertInput != 0) || (horizInput != 0) && (OnSlope()))
        {
            controller.Move((Vector3.down * (controller.height / 2) * slopeForce) * Time.deltaTime);
        }
    }

    private void InitiateJump () {
        if(Input.GetButtonDown("Jump") && (!isJumping))
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent() {
        float airTime = 0.0f;
        controller.slopeLimit = 90.0f;

        do {
            float jumpForce = jumpCurve.Evaluate(airTime);
            airTime = Jump(airTime, jumpForce);
            yield return null;

        } while ((!controller.isGrounded) && (controller.collisionFlags != CollisionFlags.Above));

        isJumping = false;
        controller.slopeLimit = 45.0f;
    }

    
}
