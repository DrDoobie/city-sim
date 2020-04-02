using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float wanderTime, wanderRadius, awarenessRadius;
    public Animator animator;
    public NavMeshAgent agent;

    float _wanderTime;

    void Start ()
    {
        _wanderTime = wanderTime;

        if(GetComponent<SphereCollider>())
        {
            GetComponent<SphereCollider>().radius = awarenessRadius;
        }
    }

    void Update ()
    {
        MovementController();
    }

    private void MovementController()
    {
        wanderTime -= Time.deltaTime;

        //This is handling random walking around
        if(wanderTime <= 0.0f)
        {
            Vector3 position = new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, Random.Range(-wanderRadius, wanderRadius)) + transform.position;

            agent.SetDestination(position);

            Debug.Log("Walking");
            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);

            wanderTime = _wanderTime;
        }

        //This is for checking distance between target and ai
        float dist = Vector3.Distance(agent.destination, transform.position);

        //Debug.Log(Vector3.Distance(agent.destination, transform.position));

        if(dist <= agent.stoppingDistance)
        {
            //Debug.Log("Reached destination!");
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            agent.SetDestination(other.transform.position);

            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);
        }
    }
}
