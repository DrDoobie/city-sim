using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    public Camera cam;
    public Text notificationsText;
    [HideInInspector] public Transform hitObj;
    public LayerMask layerMask;
    public InventoryObj inventory;

    float range;

    void Start()
    {
        if(GetComponent<PlayerCombat>())
        {
            range = GetComponent<PlayerCombat>().attackRange;

        } else{
            Debug.Log("Warning: no range for player raycast...");
        }
    }

    void Update()
    {
        //Save and load inventory
        if(Input.GetKeyDown(KeyCode.K))
        {
            inventory.Save();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            inventory.Load();
        }

        NotificationController();
        
        if(GameController.Instance.rtsMode)
            return;

        Cast();
    }

    void Cast()
    {
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, layerMask))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ItemPickup(hit);
            }

            hitObj = hit.transform;
        
        } else {
            hitObj = null;
        }
    }

    void ItemPickup(RaycastHit hit)
    {
        var item = hit.transform.GetComponent<Item>();

        if(item)
        {
            inventory.AddItem(item.item, 1);

            Destroy(hit.transform.gameObject);
        }
    }

    void NotificationController()
    {
        if(hitObj != null && !GameController.Instance.rtsMode)
        {
            notificationsText.text = hitObj.name;

            return;
        }

        notificationsText.text = "";
    }

    void OnApplicationQuit()
    {
        inventory.container.Clear();
    }
}
