using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPlace : MonoBehaviour
{
    public int maxEmployees, currentEmployees;
    public float resourceGenCoolDown;

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

        if(currentEmployees > 0)
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
        GameController.Instance.resourceController.resources ++;
    }
}
