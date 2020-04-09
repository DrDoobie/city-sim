using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPlace : MonoBehaviour
{
    public int maxEmployees = 3, currentEmployees = 0, reqEmployees = 1;
    public float resourceGenCoolDown = 10.0f;

    float _resourceGenCoolDown;

    void Start()
    {
        _resourceGenCoolDown = resourceGenCoolDown;
    }

    void Update()
    {
        if(GameController.Instance.resourceController.avaliableWorkers >= 1 && currentEmployees < maxEmployees)
        {
            Employ();
        }

        if(currentEmployees >= reqEmployees)
        {
            ResourceGenTimer();
        }
    }

    void Employ()
    {
        GameController.Instance.resourceController.avaliableWorkers -= 1;

        currentEmployees++;
    }

    void ResourceGenTimer()
    {
        resourceGenCoolDown -= Time.deltaTime;

        if(resourceGenCoolDown <= 0)
        {
            GenerateResources();

            resourceGenCoolDown = _resourceGenCoolDown;
        }
    }

    void GenerateResources()
    {
        GameController.Instance.resourceController.resources += (1 * currentEmployees);
    }
}
