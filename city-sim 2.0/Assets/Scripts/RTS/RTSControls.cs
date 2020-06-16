using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RTSControls : MonoBehaviour
{
    public Vector3 playerDestination;

    [Header("Animation")]
    public bool isAnimated;
    public Animator animator;
    
    Camera cam;

    void Start()
    {
        cam = GetComponent<RTSCamera>().rtsCam;
        playerDestination = transform.position;
    }

    void Update()
    {    
        if(GameController.Instance.playerAgent.enabled)
        {
            GameController.Instance.playerAgent.destination = playerDestination;

            if(isAnimated)
            {
                Animation();
            }
        }

        if(!GameController.Instance.playerUsingUI)
        {
            MovementController();
        }
    }

    void Animation()
    {
        if(GameController.Instance.playerAgent.remainingDistance > GameController.Instance.playerAgent.stoppingDistance)
        {
            //Moving
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", true);

        } else {
            //Not moving
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
        }
    }

    void MovementController()
    {
        Vector3 mousePos = Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Input.GetButtonDown("Fire1"))
        {
            if(Physics.Raycast(ray, out hit))
            {
                /*Ugly code delete soon*/if(hit.transform.gameObject.layer == 14)
                    return;

                if(GameController.Instance.rtsBuildingSystem.buildMode)
                {
                    return;
                }
                
                //Debug.Log(hit.point);

                playerDestination = hit.point;
            }
        }
    }
}
