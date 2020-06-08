using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Camera cam;
    public LayerMask layerMask;

    [Header("Attack Settings")]
    public float attackDamage = 25.0f;
    public float attackRange = 0.5f, thirdPersonAttackRange = 3.0f;
    public float attackRate = 2.0f, thirdPersonAttackRate = 1.0f; //How many times you can attack per second
    public Transform attackPoint;

    [Header("Particles")]
    public float particleDespawnDelay;
    public GameObject particleEffect;

    [Header("Animation")]
    public Animator animator;

    [HideInInspector] public float nextAttackTime = 0.0f;

    void Update()
    {
        if(Time.time <= nextAttackTime)
            return;

        if(Input.GetButton("Fire1") && !GameController.Instance.rtsMode)
        {
            FirstPersonAttack();

            nextAttackTime = Time.time + (1.0f / attackRate);
        }
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("Attack");

        nextAttackTime = Time.time + (1.0f / thirdPersonAttackRate);
    }

    public void FirstPersonAttack()
    {
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, attackRange, layerMask))
        {
            Debug.Log(("Hit " + hit.transform.name));

            GameObject particles = Instantiate(particleEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(particles, particleDespawnDelay);

            Resource resource = hit.transform.GetComponent<Resource>();

            if(resource)
            {
                resource.Damage(attackDamage);
            }
        }
    }

    public void ThirdPersonAttack()
    {
        Collider[] gotHit = Physics.OverlapSphere(attackPoint.position, thirdPersonAttackRange, layerMask);

        foreach(Collider hit in gotHit)
        {
            Debug.Log("Hit " + hit.transform.name);

            Resource resource = hit.transform.GetComponent<Resource>();

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

        Gizmos.DrawWireSphere(attackPoint.position, thirdPersonAttackRange);
    }
}
