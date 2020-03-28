using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public float time;
    public NavMeshAgent agent;
    public Transform target;

    float _time;

    // Start is called before the first frame update
    void Start()
    {
        _time = time;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if(time <= 0)
        {
            Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f)) + transform.position;

            agent.SetDestination(position);

            time = _time;
        }
    }
}
