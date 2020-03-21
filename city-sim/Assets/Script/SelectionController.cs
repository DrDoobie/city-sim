using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    public Transform selectedObj;
    public Material[] materials;
    
    string tag = "Selectable";
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
        if(Input.GetButtonDown("ModeSwitch") && (selectedObj != null))
        {
            Deselect();
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(Input.GetButtonDown("Fire1") && hit.transform.CompareTag(tag))
            {
                //This is handling deselection
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
        }
    }

    private void Deselect()
    {
        selectedObj.GetComponent<Renderer>().material = ogMaterial;

        selectedObj = null;

        return;
    }
}
