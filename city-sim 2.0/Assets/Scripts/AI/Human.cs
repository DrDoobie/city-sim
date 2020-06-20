using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
    public float maxHealth, health;
    public Transform target;

    [Header("AI Settings")]
    public bool isAggroed;
    public float aggroCoolDown = 7.5f;
    public float wanderCoolDown = 10.0f;
    public float wanderDistance = 50.0f;
    public NavMeshAgent agent;

    [Header("Animation")]
    public bool isAnimated;
    public Animator animator;

    bool isDead = false;
    float ogAggroCoolDown;
    float xValue, zValue;

    void Start()
    {
        health = maxHealth;
        ogAggroCoolDown = aggroCoolDown;

        StartCoroutine(WanderTimer());
    }

    void Update()
    {   
        HealthController();
        AggroController();

        if(isAnimated)
        {
            Animation();
        }
    }

    void HealthController()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    void AggroController()
    {
        if(isAggroed && !isDead)
        {
            agent.SetDestination(target.position);

            aggroCoolDown -= Time.deltaTime;

            if(aggroCoolDown <= 0)
            {
                //Debug.Log("Calmed down");
                aggroCoolDown = ogAggroCoolDown;

                StartCoroutine(WanderTimer());

                isAggroed = false;
            }
        }
    }

    void Aggro(Transform transform)
    {
        isAggroed = true;

        aggroCoolDown = ogAggroCoolDown;
        target = transform;
    }

    void Die()
    {
        isDead = true;

        this.gameObject.SetActive(false);
    }

    public void TakeDamage(Transform damageDoer, float val)
    {
        health -= val;

        //Start attacking whatever hit it
        Aggro(damageDoer);
    }

    void Animation()
    {
        if(isDead)
            return;

        if(agent.remainingDistance > agent.stoppingDistance)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", true);

        } else {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
        }
    }

    void Wander()
    {
        xValue = Random.Range(-wanderDistance, wanderDistance);
        zValue = Random.Range(-wanderDistance, wanderDistance);

        Vector3 currentPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //Generate new position based off current position and randomly generated values
        Vector3 newPos = new Vector3(currentPos.x + xValue, currentPos.y, currentPos.z + zValue);

        Debug.Log("Set destination to " + newPos);
        agent.SetDestination(newPos);
    }

    IEnumerator WanderTimer()
    {
        while(!isAggroed)
        {
            Wander();

            yield return new WaitForSeconds(wanderCoolDown);

            StartCoroutine(WanderTimer());
        }
    }
}
