using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    public float wanderCoolDown = 10.0f, wanderDistance = 50.0f;
    public NavMeshAgent agent;

    [Header("Animation")]
    public bool isAnimated;
    public Animator animator;

    float xValue, zValue;

    void Start()
    {
        StartCoroutine(WanderTimer());
    }

    void Update()
    {
        if(isAnimated)
        {
            Animation();
        }
    }

    void Animation()
    {
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

        agent.SetDestination(newPos);
    }

    IEnumerator WanderTimer()
    {
        Wander();

        yield return new WaitForSeconds(wanderCoolDown);

        StartCoroutine(WanderTimer());
    }
}
