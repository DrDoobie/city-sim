using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCast : MonoBehaviour
{
    public float range;
    public Camera cam;
    public Text notificationsText;
    public Transform hitObj;
    public LayerMask layerMask;

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
            //Debug.Log(hit.transform.name);

            hitObj = hit.transform;
        
        } else {
            hitObj = null;
        }
    }
}
