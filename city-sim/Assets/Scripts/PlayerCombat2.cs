using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat2 : MonoBehaviour
{
    public bool inRange;
    public float attackRange = 1.5f, attackSpeed = 2.0f, attackDamage = 15.0f;
    public Transform attackPoint;
    public LayerMask hittableLayers;
    public AudioManager audioManager;

    [Header("Animation")]
    public bool isAnimated;
    public string attackAnimation;
    public Animator animator;

    float attackCoolDown;

    void Update()
    {
        CheckRange();

        if(Input.GetButton("Fire1"))
        {
            if(isAnimated)
            {
                animator.Play(attackAnimation);

                return;
            }

            Attack();
        }
    }

    void CheckRange()
    {
        //Detect hittables in range
        Collider[] gotHit = Physics.OverlapSphere(attackPoint.position, attackRange, hittableLayers);

        //Apply damage to each hit object
        foreach(Collider hit in gotHit)
        {
            inRange = true;
        }

        if(gotHit.Length == 0)
        {
            inRange = false;
        }
    }

    public void Attack()
    {
        if(Time.time >= attackCoolDown)
        {
            //Detect hittables in range
            Collider[] gotHit = Physics.OverlapSphere(attackPoint.position, attackRange, hittableLayers);

            //Apply damage to each hit object
            foreach(Collider hit in gotHit)
            {
                Resource hitResource = hit.GetComponent<Resource>();
                TreeScript hitTree = hit.GetComponent<TreeScript>();
                BuildingGhost buildingGhost = hit.GetComponent<BuildingGhost>();

                //Getting resources
                if(hitResource)
                {
                    hitResource.health -= attackDamage;

                    audioManager.PlaySound("Hit Marker");
                }

                //Tree chopping
                if(hitTree)
                {
                    hitTree.ChopTree(attackDamage);

                    audioManager.PlaySound("Hit Marker");
                }

                //Building ghosts
                if(buildingGhost && GameController.Instance.resourceController.resources >= 1)
                {
                    buildingGhost.resources++;

                    GameController.Instance.resourceController.resources --;

                    GetComponent<AudioSource>().PlaySound("Build");
                }
            }

            attackCoolDown = Time.time + (1.0f / attackSpeed);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
