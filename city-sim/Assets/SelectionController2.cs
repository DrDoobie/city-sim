using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectionController2 : MonoBehaviour
{
    public Material[] materials;
    public Transform selectedObject;
    public string selectionTag = "Selectable", playerWalkableTag = "Terrain";
    public NavMeshAgent player;

    [SerializeField] Material ogMat;
    [SerializeField] Transform _selectedObject;

    void Update()
    {
        RayController();
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
        }
    }

    void SelectObject(Transform transform)
    {
        Debug.Log("Selected " + transform.name);
        selectedObject = transform;
    }

    void DeselectObject()
    {
        Debug.Log("Deselected object");
        _selectedObject = selectedObject;

        selectedObject = null;
    }

    void MovePlayer(Vector3 clickPos)
    {
        Debug.Log("Move player to " + clickPos);
        player.SetDestination(clickPos);
    }
}
