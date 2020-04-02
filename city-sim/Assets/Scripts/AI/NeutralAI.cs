using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeutralAI : MonoBehaviour
{
    public float wanderTime, wanderRadius;
    public Animator animator;
    public NavMeshAgent agent;

    float _wanderTime;

    // Start is called before the first frame update
    void Start()
    {
        _wanderTime = wanderTime;
    }

    // Update is called once per frame
    void Update()
    {
        WanderController();
    }

    private void WanderController()
    {
        wanderTime -= Time.deltaTime;

        if(wanderTime <= 0.0f)
        {
            Vector3 position = new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, Random.Range(-wanderRadius, wanderRadius)) + transform.position; 

            agent.SetDestination(position);

            //Debug.Log("Walking");
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
}
