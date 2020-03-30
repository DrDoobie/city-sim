using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRange = 0.5f, damage = 15.0f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

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
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        //Apply damage
        foreach(Collider enemy in hitEnemies)
        {
            //Damaging enemies
            if(enemy.GetComponent<Health>() == null)
            {
                return;
            }

            Health enemyHealth = enemy.GetComponent<Health>();

            enemyHealth.health -= damage;
            Debug.Log("Dealt " + damage + " to " + enemy.transform.name);
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
