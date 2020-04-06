using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    public Animator animator;
    public NavMashAgent agent;

    bool isAnimated = false;

    void Start(){
        if(animator != null)
        {
            isAnimated = true;
        }
    }

    void Update()
    {
        //Idle

        //Wander

        //Run

        //Attack
    }
}
