using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    public bool walking = false, fleeing = false;
    public float maxHealth = 100.0f, health;
    public Vector3 destination;
    public Animator animator;
    public NavMeshAgent agent;

    [Header("AI Settings")]
    public bool isAggresive;
    public float wanderCoolDown = 5.0f;
    [Range(0.0f, 50.0f)]
    public float wanderRadius = 15.0f;
    public float fleeTime = 3.0f;
    public float attackDamage = 15.0f, attackRate = 2.0f, attackCoolDown, attackRange = 1.0f;
    public Transform attackPoint;
    public LayerMask hittableLayers;

    [HideInInspector] public bool isAnimated = false;
    Transform enemy;

    void Start(){
        if(animator != null)
        {
            isAnimated = true;
        }

        //Idle();

        health = maxHealth; 

        StartCoroutine(Wander());   
    }

    void Update()
    {
        agent.SetDestination(destination);

        if(isAnimated)
        {
            AnimationControl();
        }

        WanderCheck();
        FleeCheck();

        //Chase 
    }

    void Idle()
    {
        //Debug.Log("Idle");
    }

    void AnimationControl()
    {
        if(walking)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);

            return;
        }

        animator.SetBool("isWalking", false);
        animator.SetBool("isIdle", true);
    }

    IEnumerator Wander()
    {
        Vector3 wanderPosition = new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, Random.Range(-wanderRadius, wanderRadius)) + transform.position; 

        //Debug.Log("Walking");
        walking = true;

        destination = wanderPosition;

        yield return new WaitForSeconds(wanderCoolDown);

        StartCoroutine(Wander());
    }

    private void WanderCheck()
    {
        float disToTarget = Vector3.Distance(agent.destination, transform.position);

        if(disToTarget <= agent.stoppingDistance)
        {
            //Debug.Log("Reached destination");
            walking = false;
        }
    }

    IEnumerator Flee()
    {
        fleeing = true;

        yield return new WaitForSeconds(fleeTime);

        fleeing = false;
    }

    void FleeCheck()
    {
        if(!fleeing)
        {
            return;
        }

        //Debug.Log("Fleeing!");
        Vector3 disFromEnemy = transform.position - enemy.position;

        Vector3 runPosition = transform.position + disFromEnemy;

        destination = runPosition;
    }

    void Attack()
    {
        //Detect hittables in range
        Collider[] gotHit = Physics.OverlapSphere(attackPoint.position, attackRange, hittableLayers);

        //Apply damage to each hit object
        foreach(Collider hit in gotHit)
        {
            //Attack player
            if(hit.gameObject.layer == 12 && isAggresive)
            {
                PlayerStats playerStats = hit.GetComponent<PlayerStats>();

               if(Time.time >= attackCoolDown)
               {
                   Debug.Log("Attacking!");
                   playerStats.TakeDamage(attackDamage);

                   attackCoolDown = Time.time + (1.0f / attackRate);
               }
            }
        }
    }

    void CheckForTarget(Transform target)
    {
        if(isAggresive)
        {
            destination = target.position;
        }
    }

    public void TakeDamage(float value, Transform target)
    {
        //Debug.Log("Taking damage!");
        health -= value;

        if(health <= 0)
        {
            Die();
        }

        enemy = target;

        if(!isAggresive)
        {
            StartCoroutine(Flee());

            return;
        }

        transform.LookAt(target);
    }

    void Die()
    {
        Debug.Log(transform.name + " died!");
        Destroy(this.gameObject);
    }

    //Visualizes the attack range/collider
    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
