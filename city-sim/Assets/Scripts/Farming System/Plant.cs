using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public float growSpeed = 0.1f;
    public Animator animator;

    void Update()
    {
        GrowSpeedController();

        animator.SetFloat("Speed", growSpeed);
    }

    void GrowSpeedController()
    {
        growSpeed = transform.parent.parent.GetComponent<FarmPlot>().soilMoisture * 1.2f;
    }

    public void Ready()
    {
        Debug.Log(transform.name + " is ready for havest");
    }
}
