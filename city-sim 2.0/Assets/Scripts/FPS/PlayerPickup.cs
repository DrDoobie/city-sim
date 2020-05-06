using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour
{
    public float range;
    public Camera cam;
    public Text notificationsText;
    public Transform hitObj;
    public LayerMask layerMask;
    public InventoryObj inventory;

    void Update()
    {
        Cast();

        if(hitObj != null)
        {
            notificationsText.text = hitObj.name;

            return;
        }

        notificationsText.text = "";
    }

    void Cast()
    {
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, layerMask))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //Pick up item
                var item = hit.transform.GetComponent<Item>();

                if(item)
                {
                    inventory.AddItem(item.item, 1);

                    Destroy(hit.transform.gameObject);
                }
            }

            hitObj = hit.transform;
        
        } else {
            hitObj = null;
        }
    }

    void OnApplicationQuit()
    {
        inventory.container.Clear();
    }
}
