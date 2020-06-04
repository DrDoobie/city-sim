using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public float detectionRange;
    public Transform detectionPoint;
    public LayerMask hittableLayers;

    void Update()
    {
        DetectionController();
    }

    void DetectionController()
    {
        Collider[] hit = Physics.OverlapSphere(detectionPoint.position, detectionRange, hittableLayers);

        foreach (Collider hittable in hit)
        {
            //Debug.Log("Hit " + hittable);

            var playerCombat = GetComponent<PlayerCombat>();
            var resource = hittable.GetComponent<Resource>();

            if(Time.time <= playerCombat.nextAttackTime)
                return;

            if(resource)
                playerCombat.TriggerAttack();
        }
    }
}
