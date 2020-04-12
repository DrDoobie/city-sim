using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHarvest : MonoBehaviour
{
    public string attackAnimation;
    public Animator animator;

    void Update()
    {
        if(GameController.Instance.rtsMode)
        {
            HarvestController();
        }
    }

    private void HarvestController()
    {
        PlayerCombat playerComb = GetComponent<PlayerCombat>();

        if(playerComb)
        {
            if(playerComb.inRange)
            {
                animator.Play(attackAnimation);
            }
        }
    }
}
