using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    public int reqResources;
    public Text buildText;
    public Material[] ghostMaterial;
    public GameObject ghostObj;
    public GameObject objToPlace;
    public GameObject[] objects;

    int lastPosX, lastPosY, lastPosZ, obj;
    float lastScroll;
    Vector3 mousePos, lastPos;

    void Start ()
    {
        SetGhost();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.Instance.buildMode && GameController.Instance.rtsMode)
        {
            ghostObj.SetActive(true);

            BuildController();

            return;
        }

        buildText.text = "";
        ghostObj.SetActive(false);
    }

    private void BuildController()
    {
        SwitchObj();

        mousePos = Input.mousePosition;

        buildText.text = objToPlace.name;
        buildText.transform.position = mousePos;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            int posX = (int)Mathf.Round(hit.point.x);
            int posZ = (int)Mathf.Round(hit.point.z);

            if(posX != lastPosX || posZ != lastPosZ)
            {
                lastPosX = posX;
                lastPosZ = posZ;

                ghostObj.transform.position = new Vector3(posX, 0.0f, posZ);
            }

            if(Input.GetButtonDown("Fire1"))
            {
                PlaceObj();
            }
        }
    }
    
    private void SwitchObj()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        
        if(scroll != lastScroll)
        {
            lastScroll = scroll;
            
            SetGhost();
        }

        if(scroll > 0)
        {
            obj++;
        }

        if(scroll < 0)
        {
            obj--;
        }

        //Clamp
        obj = Mathf.Clamp(obj, 0, (objects.Length - 1));

        objToPlace = objects[obj];
    }

    private void PlaceObj()
    {
        if(GameController.Instance.resourceController.resources >= reqResources)
        {
            if(ghostObj.GetComponent<GhostObject>().canPlace)
            {
                GameObject go = Instantiate(objToPlace, ghostObj.transform.position, Quaternion.identity);
        
                GameController.Instance.resourceController.resources--;

                Debug.Log("Placed " + objToPlace + " at position " + go.transform.position);
                return;
            }

            Debug.Log("Error: not enough space to place");
            return;
        }

        Debug.Log("Error: couldn't afford to place");
    }

    private void SetGhost()
    {
        if(ghostObj != null)
        {
            lastPos = ghostObj.transform.position;

            Destroy(ghostObj);

            ghostObj = null;
        }

        ghostObj = objects[obj];

        GameObject go = Instantiate(ghostObj, lastPos, Quaternion.identity);

        go.AddComponent(typeof(GhostObject));
    
        ghostObj = go;
    }
}
