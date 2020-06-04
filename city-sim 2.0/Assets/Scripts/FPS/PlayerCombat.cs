using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackDamage = 25.0f, attackRange = 0.5f;
    public float attackRate = 2.0f; //How many times you can attack per second
    public Transform attackPoint;
    public LayerMask hittableLayers;

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
        Collider[] hit = Physics.OverlapSphere(attackPoint.position, attackRange, hittableLayers);

        foreach(Collider hittable in hit)
        {
            Debug.Log("Hit " + hittable);

            Resource resource = hittable.GetComponent<Resource>();

            if(resource)
            {
                resource.Damage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
