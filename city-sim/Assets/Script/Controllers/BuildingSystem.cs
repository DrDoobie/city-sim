using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public int reqResources;
    public Material ghostMaterial;
    public GameObject ghostObj;
    public GameObject objToPlace;
    public GameObject[] objects;

    int lastPosX, lastPosY, lastPosZ;
    [SerializeField] int obj;
    Vector3 mousePos;

    void Start ()
    {
        SetGhost();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchObj();

        if(GameController.Instance.buildMode && GameController.Instance.rtsMode)
        {
            ghostObj.GetComponent<MeshRenderer>().enabled = true;

            BuildController();

            return;
        }

        ghostObj.GetComponent<MeshRenderer>().enabled = false;
    }

    private void BuildController()
    {
        mousePos = Input.mousePosition;

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

    private void SetGhost()
    {
        GameObject go = Instantiate(objToPlace);

        go.GetComponent<Renderer>().material = ghostMaterial;

        if(go.GetComponent<MeshCollider>())
        {
            go.GetComponent<MeshCollider>().convex = true;
            go.GetComponent<MeshCollider>().isTrigger = true;
        }

        go.layer = 2;

        ghostObj = go;
    }

    private void SwitchObj()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
    
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
            Instantiate(objToPlace, ghostObj.transform.position, Quaternion.identity);
        
            GameController.Instance.resourceController.resources--;
        }
    }
}
