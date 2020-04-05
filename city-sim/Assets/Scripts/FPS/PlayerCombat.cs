using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool inRange;
    public float attackRange = 0.5f, damage = 15.0f, attackRate = 2.0f;
    public Transform attackPoint;
    public Animator animator;
    public LayerMask hittableLayers;

    float nextAttackTime = 0.0f;

    void Update()
    {
        CheckRange();

        if(Time.time >= nextAttackTime)
        {
            if(Input.GetButtonDown("Fire1") && !GameController.Instance.rtsMode)
            {
                animator.Play("Punch");

                nextAttackTime = Time.time + (1.0f / attackRate);
            }
        }               
    }

    public void Attack()
    {
        //Detect hittables in range
        Collider[] gotHit = Physics.OverlapSphere(attackPoint.position, attackRange, hittableLayers);

        //Apply damage to each hit object
        foreach(Collider hit in gotHit)
        {
            //Build ghost
            if(hit.GetComponent<BuildingGhost>())
            {
                hit.GetComponent<BuildingGhost>().resources++;

                GameController.Instance.resourceController.resources --;
            }

            //Get resources
            if(hit.GetComponent<Resource>())
            {
                Debug.Log("Harvesting resource!");
                hit.GetComponent<Resource>().health -= damage;
            }

            //Damage enemy
            if(hit.GetComponent<Health>() == null)
            {
                return;
            }

            Health enemyHealth = hit.GetComponent<Health>();

            enemyHealth.health -= damage;
            Debug.Log("Dealt " + damage + " dmg to " + hit.transform.name);
        }
    }

    public void CheckRange()
    {
        //Detect hittables in range
        Collider[] gotHit = Physics.OverlapSphere(attackPoint.position, attackRange, hittableLayers);

        //Apply damage to each hit object
        foreach(Collider hit in gotHit)
        {
            inRange = true;
        }

        if(gotHit.Length == 0)
        {
            inRange = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
