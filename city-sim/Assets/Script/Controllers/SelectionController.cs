using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    public ResourceController resourceController;
    public Transform selectedObj;
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
                if (selectedObj != null)
                {
                    _selectedObj = selectedObj;

                    _selectedObj.GetComponent<Renderer>().material = ogMaterial;
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

            //Right click
            if(Input.GetButtonDown("Fire2") && hit.transform.CompareTag(reqTag))
            {
                if(selectedObj != null && (hit.transform == selectedObj))
                {
                    Harvest();
                }
            }
        }
    }

    private void Harvest()
    {
        Destroy(selectedObj.gameObject);
        
        selectedObj = null;

        resourceController.resources++;
    }

    public void Deselect()
    {
        selectedObj.GetComponent<Renderer>().material = ogMaterial;

        selectedObj = null;
    }
}
