using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRange = 0.5f, damage = 15.0f;
    public Transform attackPoint;
    public LayerMask hittableLayers;

    // Update is called once per frame
    void Update()
    {
        if(!GameController.Instance.rtsMode)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        //Detect enemies in range
        Collider[] gotHit = Physics.OverlapSphere(attackPoint.position, attackRange, hittableLayers);

        //Apply damage to eat hit object
        foreach(Collider hit in gotHit)
        {
            //Get resources
            if(hit.GetComponent<Resource>())
            {
                //Debug.Log("Getting resource!");
                hit.GetComponent<Resource>().health -= damage;
            }

            //Damage enemy
            if(hit.GetComponent<Health>() == null)
            {
                return;
            }

            Health enemyHealth = hit.GetComponent<Health>();

            enemyHealth.health -= damage;
            Debug.Log("Dealt " + damage + " to " + hit.transform.name);
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
