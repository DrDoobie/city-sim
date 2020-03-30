using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    public bool genResources, genFood;
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

    void Function()
    {
        if(genResources)
        {
            GameController.Instance.resourceController.resources++;
        }

        if(genFood)
        {
            GameController.Instance.resourceController.food++;
        }
    }
}
