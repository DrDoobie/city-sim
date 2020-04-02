using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeutralAI : MonoBehaviour
{
    public bool baby, male, female, givenBirth;
    public float wanderTime, wanderRadius, awarenessRadius;
    public Animator animator;
    public NavMeshAgent agent;
    public Creature creature;

    float growTime, birthCoolDown;
    float _wanderTime, _growTime, _birthCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        baby = true;
        
        if(GetComponent<SphereCollider>())
        {
            GetComponent<SphereCollider>().radius = awarenessRadius;
        }

        SetValues();
        AssignGender();
    }

    // Update is called once per frame
    void Update()
    {
        WanderController();
        BirthController();

        if(baby)
        {
            growTime -= Time.deltaTime;

            if(growTime <= 0.0f)
            {
                baby = false;

                //Debug.Log("Grown up!");

                growTime = 0.0f;
            }
        }
    }

    void SetValues()
    {
        growTime = creature.growTime;
        birthCoolDown = creature.birthCoolDown;

        _wanderTime = wanderTime;
        _growTime = growTime;
        _birthCoolDown = birthCoolDown;
    }

    private void AssignGender()
    {
        int value = Random.Range(0, 2);
        //Debug.Log(value);

        if (value == 1)
        {
            male = true;

            female = false;

            return;
        }

        female = true;

        male = false;
    }

    private void WanderController()
    {
        wanderTime -= Time.deltaTime;

        if(wanderTime <= 0.0f)
        {
            Vector3 position = new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, Random.Range(-wanderRadius, wanderRadius)) + transform.position; 

            agent.SetDestination(position);

            //Debug.Log("Walking");
            animator.Play("Walk");

            wanderTime = _wanderTime;
        }

        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            //Debug.Log("Reached destination!");
            animator.Play("Idle");
        }
    }

    private void BirthController()
    {
        if(givenBirth)
        {
            birthCoolDown -= Time.deltaTime;

            if(birthCoolDown <= 0.0f)
            {
                givenBirth = false;
                //Debug.Log("Ready to give birth again");

                birthCoolDown = _birthCoolDown;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<NeutralAI>())
        {
            NeutralAI otherAI = other.GetComponent<NeutralAI>();
            Debug.Log("Met " + other.transform.name);

            if(!baby)
            {
                if(female)
                {
                    if(otherAI.male)
                    {
                        Birth();
                    }
                }
            }
        }
    }

    void Birth()
    {
        if(!givenBirth)
        {
            //Debug.Log("Gave birth!");
            Instantiate(creature.baby, transform.position, Quaternion.identity);

            givenBirth = true;
        }
    }
}
