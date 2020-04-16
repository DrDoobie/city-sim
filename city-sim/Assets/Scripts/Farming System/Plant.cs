using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public float value, growSpeed = 0.1f, growTime = 15.0f;
    public Animator animator;

    void Update()
    {
        GrowSpeedController();

        animator.SetFloat("Speed", growSpeed);
    }

    void GrowSpeedController()
    {
        growSpeed = transform.parent.parent.GetComponent<FarmPlot>().soilMoisture * 1.2f;

        growTime -= Time.deltaTime;

        if(growTime <= 0)
        {
            ReadyForHarvest();
        }
    }

    public void ReadyForHarvest()
    {
        //Debug.Log("Ready for harvest");
        gameObject.layer = 9;
    }

    public void Harvest()
    {
        if(GameController.Instance.resourceController.food < GameController.Instance.resourceController.maxFood)
        {
            //Debug.Log("Harvest");
            GameController.Instance.resourceController.food += value;

            Destroy(this.gameObject);

            return;
        }

        Debug.Log("Not enough food storage avaliable");
    }
}
