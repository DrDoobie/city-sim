﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackDamage = 25.0f, attackRange = 0.5f;
    public float attackRate = 2.0f; //How many times you can attack per second
    public Transform attackPoint;
    public Camera cam;

    [Header("Particles")]
    public float particleDespawnDelay;
    public ParticleSystem particleEffect;

    [Header("Animation")]
    public Animator animator;

    [HideInInspector] public float nextAttackTime = 0.0f;

    void Update()
    {
        if(Time.time <= nextAttackTime)
            return;

        if(Input.GetButton("Fire1") && !GameController.Instance.rtsMode)
        {
            TriggerAttack();
        }
    }

    public void TriggerAttack()
    {
        //Attack();

        animator.SetTrigger("Attack");

        nextAttackTime = Time.time + (1.0f / attackRate);
    }

    public void Attack()
    {
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, attackRange))
        {
            Debug.Log(("Hit " + hit.transform.name));

            //Gathering resources
            Resource resource = hit.transform.GetComponent<Resource>();

            if(resource)
            {
                /*if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * 30.0f);
                }*/

                GameObject go = Instantiate(particleEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(go, particleDespawnDelay);
                
                resource.Damage(attackDamage);
            }
        }
    }

    /*void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }*/
}
