using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
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
            Function();

            time = _time;
        }
    }

    private static void Function()
    {
        GameController.Instance.resourceController.resources++;
    }
}
