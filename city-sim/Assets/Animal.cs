﻿using System.Collections;
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
    public float wanderCoolDown = 5.0f;
    [Range(0.0f, 50.0f)]
    public float wanderRadius = 15.0f;
    [Range(0.0f, 50.0f)]
    public float awarenessRadius = 15.0f;
    public float fleeTime = 3.0f;

    bool isAnimated = false;
    Transform enemy;

    void Start(){
        if(animator != null)
        {
            isAnimated = true;
        }

        Idle();

        health = maxHealth; 

        StartCoroutine(Wander());   
    }

    void Update()
    {
        agent.SetDestination(destination);

        WanderCheck();
        FleeCheck();

        //Attack
    }

    void Idle()
    {
        //Debug.Log("Idle");
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

    public void TakeDamage(float value, Transform target)
    {
        Debug.Log("Taking damage!");
        health -= value;

        enemy = target;

        StartCoroutine(Flee());
    }
}
