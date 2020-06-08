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
    public GameObject[] particleEffect;

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
            //Debug.Log(("Hit " + hit.transform.name));

            HitAI(hit);
            GatherResource(hit);
        }
    }

    void HitAI(RaycastHit hit)
    {
        Animal animal = hit.transform.GetComponent<Animal>();
        Human human = hit.transform.GetComponent<Human>();

        if(animal || human)
        {
            GameObject particles = Instantiate(particleEffect[1], hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(particles, particleDespawnDelay);

            Debug.Log("Hit an AI!");
        }
    }

    void GatherResource(RaycastHit hit)
    {
        Resource resource = hit.transform.GetComponent<Resource>();

        if(resource)
        {
            GameObject particles = Instantiate(particleEffect[0], hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(particles, particleDespawnDelay);

            resource.Damage(attackDamage);
        }
    }

    public void ThirdPersonAttack()//In the future try to make both attacks the same function and use some boolean logic ?
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
