using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public bool baby, male, female, givenBirth;
    public float wanderTime, growTime, birthCoolDown;
    public NavMeshAgent agent;
    public GameObject babyAI;

    float _wanderTime, _birthCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        baby = true;

        _wanderTime = wanderTime;
        _birthCoolDown = birthCoolDown;

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

                Debug.Log("grown up");

                growTime = 0.0f;
            }
        }
    }

    private void AssignGender()
    {
        int value = Random.Range(0, 2);
        Debug.Log(value);

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
            Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f)) + transform.position;

            agent.SetDestination(position);

            wanderTime = _wanderTime;
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
                Debug.Log("Ready to give birth again");

                birthCoolDown = _birthCoolDown;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<AI>())
        {
            AI otherAI = other.GetComponent<AI>();
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
            Debug.Log("Sex!");
            Instantiate(babyAI, transform.position, Quaternion.identity);

            givenBirth = true;
        }
    }
}
