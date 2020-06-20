using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    public float maxHealth, health;

    [Header("AI Settings")]
    //public bool isFleeing;
    public float wanderCoolDown = 10.0f;
    public float wanderDistance = 50.0f;
    public NavMeshAgent agent;

    [Header("Animation")]
    public bool isAnimated;
    public Animator animator;

    bool isDead = false;
    float xValue, zValue;

    void Start()
    {
        health = maxHealth;

        StartCoroutine(WanderTimer());
    }

    void Update()
    {
        HealthController();

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

    void Die()
    {
        isDead = true;

        this.gameObject.SetActive(false);
    }

    public void TakeDamage(float val)
    {
        health -= val;

        Flee();
    }

    void Flee()
    {
        //isFleeing = true;

        Vector3 runDirection = transform.position - GameObject.FindWithTag("Player").transform.position;

        Vector3 checkPos = transform.position + runDirection; //This is working but the ai only runs for like a second prolly cause it needs to be called in an update function

        //Reset wanderCoolDown maybe
        
        agent.SetDestination(checkPos); 

        Debug.Log("Fleeing from player!");
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
        /*if(isFleeing)
            return;*/

        xValue = Random.Range(-wanderDistance, wanderDistance);
        zValue = Random.Range(-wanderDistance, wanderDistance);

        Vector3 currentPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //Generate new position based off current position and randomly generated values
        Vector3 newPos = new Vector3(currentPos.x + xValue, currentPos.y, currentPos.z + zValue);

        agent.SetDestination(newPos);
    }

    IEnumerator WanderTimer()
    {
        Wander();

        yield return new WaitForSeconds(wanderCoolDown);

        StartCoroutine(WanderTimer());
    }
}
