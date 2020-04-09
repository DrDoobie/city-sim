using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SelectionController : MonoBehaviour
{
    public Material[] materials;
    public Transform selectedObject;
    public string selectionTag = "Selectable", playerWalkableTag = "Terrain";
    public NavMeshAgent player;

    Material ogMat, ogMat2;
    Transform _selectedObject;

    void Update()
    {
        if(GameController.Instance.rtsMode)
        {
            RayController();
        }
    }

    void RayController()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(Input.GetButtonDown("Fire1"))
            {
                if(hit.transform.CompareTag(selectionTag) && hit.transform != selectedObject)
                {   
                    if(selectedObject != null)
                    {
                        DeselectObject();
                    }

                    SelectObject(hit.transform);

                    return;
                }

                if(hit.transform == selectedObject)
                {
                    DeselectObject();

                    return;
                }

                if(hit.transform.CompareTag(playerWalkableTag))
                {
                    MovePlayer(hit.point);
                }
            }

            if(Input.GetButtonDown("Fire2"))
            {
                if(hit.transform == selectedObject)
                {
                    RemoveObject(hit.transform.gameObject);
                }
            }
        }
    }

    void SelectObject(Transform transform)
    {
        //Debug.Log("Selected " + transform.name);
        selectedObject = transform;

        SetMaterial();
    }

    void SetMaterial()
    {
        Renderer rend = selectedObject.GetComponent<Renderer>();

        ogMat = rend.material;

        if(rend.materials.Length > 1)
        {
            ogMat2 = rend.materials[1];

            //Debug.Log("More than 1 material active");
            Material[] mats;

            mats = rend.materials;

            mats[0] = materials[0];
            mats[1] = materials[0];

            rend.materials = mats;

            return;
        }

        rend.material = materials[0];
    }
    
    void ResetMaterial()
    {
        //Debug.Log("Reset material!");
        if(_selectedObject.GetComponent<Renderer>().materials.Length > 1)
        {
            //Debug.Log("More than 1 material active");
            Material[] mats;

            mats = _selectedObject.GetComponent<Renderer>().materials;

            mats[0] = ogMat;
            mats[1] = ogMat2;

            _selectedObject.GetComponent<Renderer>().materials = mats;

            return;
        }

        _selectedObject.GetComponent<Renderer>().material = ogMat;
    }

    public void DeselectObject()
    {
        //Debug.Log("Deselected object");
        _selectedObject = selectedObject;

        ResetMaterial();

        selectedObject = null;
    }

    void RemoveObject(GameObject obj)
    {
        GetComponent<ResourceController>().resources++;

        Destroy(obj);
    }

    void MovePlayer(Vector3 clickPos)
    {
        //Debug.Log("Move player to " + clickPos);
        player.SetDestination(clickPos);
    }
}
