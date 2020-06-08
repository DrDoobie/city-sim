using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSRaycast : MonoBehaviour
{
    public LayerMask rightClickLayerMask;
    public GameObject selected;

    Camera cam;

    void Start()
    {
        cam = GetComponent<RTSCamera>().rtsCam;
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            LeftClick();
        }

        if(Input.GetButtonDown("Fire2"))
        {
            RightClick();
        }
    }

    void LeftClick()
    {
        RaycastHit hit;
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Interactions with flag object
            var flag = hit.transform.GetComponent<Flag>();

            if(flag)
            {
                flag.UseFlag();
            }

            /*var resource = hit.transform.GetComponent<Resource>();

            if(resource)
            {
                selectedObject = hit.transform.gameObject;
            }*/
        }
    }

    void RightClick()
    {
        RaycastHit hit;

        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, rightClickLayerMask))
        {
            Debug.Log(hit.transform.name);

            selected = hit.transform.gameObject;

            SelectionController();
        }
    }

    void SelectionController()
    {
        GameObject dropDownMenu = GameController.Instance.dropDown;

        if(selected != null)
        {
            dropDownMenu.GetComponent<DropdownManager>().MoveToCursor();

            dropDownMenu.SetActive(true);
        }
    }
}
