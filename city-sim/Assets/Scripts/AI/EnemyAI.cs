using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public float wanderTime, wanderRadius, awarenessRadius;
    public Transform target;
    public GameObject ui;
    public Text infoText;
    public NavMeshAgent agent;
    public Creature creature;

    float _wanderTime;

    void Start ()
    {
        _wanderTime = wanderTime;

        infoText.text = creature.info;

        if(GetComponent<SphereCollider>())
        {
            GetComponent<SphereCollider>().radius = awarenessRadius;
        }
    }

    void Update ()
    {
        UIController();
        WanderController();
    }

    private void UIController()
    {
        if(GameController.Instance.selectionController.selectedObj == this.gameObject.transform)
        {
            ui.SetActive(true);

            return;
        }

        ui.SetActive(false);
    }

    private void WanderController()
    {
        wanderTime -= Time.deltaTime;

        if(wanderTime <= 0.0f)
        {
            Vector3 position = new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, Random.Range(-wanderRadius, wanderRadius)) + transform.position;

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
