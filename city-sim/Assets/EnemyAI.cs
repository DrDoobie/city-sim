using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public bool aggro;
    public Transform target;
    public Text infoText;
    public NavMeshAgent agent;
    public Creature creature;

    float wanderTime, _wanderTime;

    void Start ()
    {
        wanderTime = creature.wanderTime;
        _wanderTime = wanderTime;
    }

    void Update ()
    {
        TextController();

        //if(!aggro)
        //{
            WanderController();

        //    return;
        //}

        //TargetController();
    }

    private void TextController()
    {
        if(GameController.Instance.selectionController.selectedObj == this.gameObject.transform)
        {
            infoText.text = creature.info;

            return;
        }

        infoText.text = "";
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

    private void TargetController()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
            //Debug.Log("Set target to " + target.name);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player" || other.transform.tag == "Creature")
        {
            //aggro = true;

            target = other.transform;    

            TargetController();
        }
    }


}
