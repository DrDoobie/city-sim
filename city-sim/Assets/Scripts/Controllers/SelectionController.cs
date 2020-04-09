using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectionController : MonoBehaviour
{
    public string[] otherTag;
    public Transform selectedObj;
    public NavMeshAgent player;
    public Material[] materials;
    
    string reqTag = "Selectable";
    Transform _selectedObj;
    Material ogMaterial;

    // Update is called once per frame
    void Update()
    {
        if(!GameController.Instance.buildMode && GameController.Instance.rtsMode)
        {
            SelectionSystem();

            player.enabled = true;

            return;
        }

        player.enabled = false;

        if(selectedObj != null)
        {
            Deselect();
        }
    }

    private void SelectionSystem()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            //Left click
            if(Input.GetButtonDown("Fire1") && hit.transform.CompareTag(reqTag))
            {
                //This is handling deselection when you click the selected object
                if(hit.transform == selectedObj)
                {
                    Deselect();
                    return;
                }

                //Storing last selected object values
                if(selectedObj != null)
                {
                    _selectedObj = selectedObj;

                    if(_selectedObj.GetComponent<Renderer>())
                    {
                        _selectedObj.GetComponent<Renderer>().material = ogMaterial;
                    } 
                }

                //Selecting new object
                selectedObj = hit.transform;

                Renderer selectionRenderer = selectedObj.GetComponent<Renderer>();

                if(selectionRenderer != null)
                {
                    ogMaterial = selectionRenderer.material; 
                        
                    selectionRenderer.material = materials[0];
                }
            }

            //Point and click movement
            if(Input.GetButtonDown("Fire1"))
            {
                if(selectedObj == null)
                {
                    //Debug.Log(hit.point);
                    player.SetDestination(hit.point);
                }
            }

            //Right click
            if(Input.GetButtonDown("Fire2") && hit.transform.CompareTag(reqTag))
            {
                if(selectedObj != null && (hit.transform == selectedObj))
                {
                    RemoveObject();
                }
            }
        }
    }

    private void RemoveObject()
    {
        GetComponent<ResourceController>().resources++;

        Destroy(selectedObj.gameObject);
    }

    public void Deselect()
    {
        if(selectedObj.GetComponent<Renderer>())
        {
            selectedObj.GetComponent<Renderer>().material = ogMaterial;
        }

        selectedObj = null;
    }
}
