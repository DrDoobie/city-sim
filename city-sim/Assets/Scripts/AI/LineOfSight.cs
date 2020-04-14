using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public float viewRadius;
    [Range(0.0f, 360.0f)]
    public float viewAngle;
    public LayerMask targetMask, obstacleMask;
    public List<Transform> visibleTargets = new List<Transform>();

    void Update()
    {
        FindVisibleTargets();
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius [i].transform;

            Vector3 dirToTarget = (target.position - transform.position).normalized; 

            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle/2.0f)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0.0f ,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
