using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimation : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            animator.CrossFade("Walk", 0.5f);
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", true);

            return; 
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            animator.CrossFade("Die", 0.5f);

            return;
        }

        animator.CrossFade("Idle", 0.5f);
        animator.SetBool("isIdle", true);
        animator.SetBool("isWalking", false);
    }
}
