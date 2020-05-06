using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerper : MonoBehaviour
{
    public float speed = 1.0f;
    public Transform startPos, endPos;

    float startTime, totalDistance;

    void Start()
    {
        //Check start time
        startTime = Time.time;

        //Calculate total distance
        totalDistance = Vector3.Distance(startPos.position, endPos.position);
    }

    void Update()
    {
        LerpObj();
    }

    void LerpObj()
    {
        //Distance moved = elapsed time * speed
        float distMoved = (Time.time - startTime) * speed;

        //Part of journey completed = current distance / total distance
        float partOfJourney = distMoved / totalDistance;

        this.transform.position = Vector3.Lerp(startPos.position, endPos.position, partOfJourney);
    }
}
