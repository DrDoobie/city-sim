using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public int value;
    public float health, despawnDelay, treeFallForce;

    bool rigidBodyMade;

    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    public void ChopTree(float value)
    {
        PlayerCombat playerCombat = FindObjectOfType<PlayerCombat>();

        if(playerCombat.itemInHand != null && playerCombat.itemInHand.GetComponent<UseableItem>().item.itemType == "axe" && health >= 0)
        {
            health -= value;

            return;
        }

        Debug.Log("Incorrect item type...");
    }

    void Die()
    {
        if(!rigidBodyMade)
        {
            Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();

            rigidBody.AddForce(transform.forward * treeFallForce);

            GameController.Instance.resourceController.resources += value;

            rigidBodyMade = true;
        }

        Destroy(this.gameObject, despawnDelay);
    }
}
