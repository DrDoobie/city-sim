using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float time;
    float _time;

    void Start () {
        _time = time;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if(time <= 0)
        {
            GameController.Instance.resourceController.resources++;

            time = _time;
        }
    }
}
