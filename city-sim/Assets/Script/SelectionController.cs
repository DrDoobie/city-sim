using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    public Transform selectedObj;
    public Material[] materials;
    
    string tag = "Selectable";
    [SerializeField] Transform _selectedObj;
    [SerializeField] Material ogMaterial;

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
            if(Input.GetButtonDown("Fire1") && hit.transform.CompareTag(tag))
            {
                if(hit.transform == selectedObj)
                {
                    selectedObj.GetComponent<Renderer>().material = ogMaterial;

                    selectedObj = null;

                    return;
                }

                if(selectedObj != null)
                {
                    _selectedObj = selectedObj;

                    _selectedObj.GetComponent<Renderer>().material = ogMaterial;
                }

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
}
