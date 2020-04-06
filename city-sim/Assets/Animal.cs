using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    public bool idle;
    public float wanderCoolDown, wanderRadius;
    public Vector3 destination;
    public Animator animator;
    public NavMeshAgent agent;

    bool isAnimated = false;
    float _wanderCoolDown;

    void Start(){
        if(animator != null)
        {
            isAnimated = true;
        }

        Idle();

        _wanderCoolDown = wanderCoolDown;
    }

    void Update()
    {
        agent.SetDestination(destination);

        Wander();

        //Run away

        //Attack
    }

    void Idle()
    {
        Debug.Log("Idle");
        idle = true;
    }

    void Wander()
    {
        idle = false;

        wanderCoolDown -= Time.deltaTime;

        if(wanderCoolDown <= 0.0f)
        {
            Vector3 wanderPosition = new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, Random.Range(-wanderRadius, wanderRadius)) + transform.position; 

            Debug.Log("Walking");
            destination = wanderPosition;

            wanderCoolDown = _wanderCoolDown;
        }
    }
}
