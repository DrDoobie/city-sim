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
        /*if(Input.GetButtonDown("Fire1"))
        {
            LeftClick();
        }*/

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
            //Debug.Log(hit.transform.name);
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
            GameController.Instance.playerUsingUI = true;

            dropDownMenu.GetComponent<DropdownManager>().MoveToCursor();

            dropDownMenu.SetActive(true);
        }
    }
}
