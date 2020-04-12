using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool inRange;
    public AudioManager audioManager;

    [Header("Hand")]
    public Transform itemInHand;
    public Transform hand;
    public Vector3 itemPosition;
    public Quaternion itemRotation;

    [Header("Attack")]
    public float attackRange = 1.5f;
    public float attackSpeed = 2.0f;
    public float attackDamage, handDamage = 15.0f;
    public Transform attackPoint;
    public LayerMask hittableLayers;

    [Header("Animation")]
    public bool isAnimated;
    public string attackAnimation;
    public Animator animator;

    float attackCoolDown;

    void Update()
    {
        CheckRange();
        CombatController();
        HandController();

        if(!GameController.Instance.rtsMode)
        {
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
                Animal animal = hit.GetComponent<Animal>();
                UseableItem item= hit.GetComponent<UseableItem>();

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

                    audioManager.PlaySound("Build");
                }

                //Hit animal
                if(animal)
                {
                    animal.TakeDamage(attackDamage, this.transform);

                    audioManager.PlaySound("Smack");

                    //Debug.Log("Dealt " + damage + " dmg to " + hit.transform.name);
                }

                //Pickup item
                if(item)
                {
                    Debug.Log("!");
                    item.pickedUp = true;

                    itemInHand = hit.transform;
                }
            }

            attackCoolDown = Time.time + (1.0f / attackSpeed);
        }
    }

    void CombatController()
    {
        if(itemInHand != null)
        {
            attackDamage = itemInHand.GetComponent<UseableItem>().item.damage;

            return;
        }

        attackDamage = handDamage;
    }

    void HandController()
    {
        if(Input.GetButtonDown("Drop Item"))
        {
            DropItemInHand();
        }

        if(itemInHand == null)
        {
            return;
        }

        itemInHand.parent = hand;

        itemInHand.localPosition = itemPosition;

        itemInHand.localRotation = itemRotation;
    }

    void DropItemInHand()
    {
        if(itemInHand != null)
        {
            itemInHand.GetComponent<UseableItem>().pickedUp = false;

            itemInHand.parent = null;

            itemInHand = null;
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
