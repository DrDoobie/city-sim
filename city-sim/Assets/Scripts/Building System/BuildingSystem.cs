using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    public AudioManager audioManager;
    public WallBuilding wallBuilding;
    public float rotateAngle;
    public GameObject objToPlace;
    public GameObject[] rtsObjects;
    
    [Header("Ghost")]
    public GameObject ghostObj;
    public Material[] ghostMaterial;


    bool rotX, rotY, rotZ;
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

        GameController.Instance.infoText.text = "";
        
        ghostObj.SetActive(false);
    }

    void SetGhost()
    {
        if(ghostObj != null)
        {
            lastPos = ghostObj.transform.position;

            Destroy(ghostObj);

            ghostObj = null;
        }

        ghostObj = rtsObjects[obj];

        GameObject go = Instantiate(ghostObj, lastPos, ghostObj.transform.rotation);

        go.AddComponent(typeof(GhostObject));
    
        ghostObj = go;
    }

    void BuildController()
    {
        SwitchObj();

        mousePos = Input.mousePosition;

        //Text ui stuff
        ObjectInfo objInfo = objToPlace.GetComponent<ObjectInfo>();

        if(objInfo != null)
        {
            GameController.Instance.infoText.text = objInfo.obj.objInfo;

            GameController.Instance.infoText.transform.position = mousePos;

        } else {
            Debug.Log("No object data!");
        }

        WallBuildingController();

        //Movement of cursor/ghost
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

            //Rotation
            rotX = objInfo.obj.rotX;

            rotY = objInfo.obj.rotY;

            rotZ = objInfo.obj.rotZ;

            if(Input.GetButtonDown("Rotate"))
            {
                if(rotX)
                {
                    ghostObj.transform.Rotate(Vector3.left, rotateAngle);
                }

                if(rotY)
                {
                    ghostObj.transform.Rotate(Vector3.up, rotateAngle);
                }

                if(rotZ)
                {
                    ghostObj.transform.Rotate(Vector3.forward, rotateAngle);
                }
            }

            if(Input.GetButtonDown("Fire1"))
            {
                CheckPlace();
            }
        }
    }
    
    void SwitchObj()
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
        if(obj > (rtsObjects.Length - 1))
        {
            obj = 0;
        }

        if(obj < 0)
        {
            obj = (rtsObjects.Length - 1);
        }

        //obj = Mathf.Clamp(obj, 0, (rtsObjects.Length - 1));

        objToPlace = rtsObjects[obj];
    }

    void WallBuildingController()
    {
        if(objToPlace.GetComponent<ObjectInfo>().obj.objType == "Wall")
        {
            wallBuilding.enabled = true;

            return;   
        }

        wallBuilding.enabled = false;
    }

    void CheckPlace()
    {
        Object obj = objToPlace.GetComponent<ObjectInfo>().obj;
        ResourceController resourceController = GameController.Instance.resourceController;
        GhostObject gObject = ghostObj.GetComponent<GhostObject>();

        if(obj.objType == "Wood" && resourceController.wood >= obj.cost)
        {
            if(gObject.canPlace)
            {
                PlaceObj(obj);

                return;
            }

            Debug.Log("Error: not enough space to place");
            return;
        }

        if(obj.objType == "Stone" && resourceController.stone >= obj.cost)
        {
            if(gObject.canPlace)
            {
                PlaceObj(obj);

                return;
            }

            Debug.Log("Error: not enough space to place");
            return;
        }
            
        Debug.Log("Error: couldn't afford to place");
    }

    void PlaceObj(Object obj)
    {
        ResourceController resourceController = GameController.Instance.resourceController;
        ResourceStorage resourceStorage = objToPlace.GetComponent<ResourceStorage>();

        if(resourceStorage)
        {
            resourceStorage.AddToStorage();
        }

        GameObject go = Instantiate(objToPlace, ghostObj.transform.position, ghostObj.transform.rotation);
                
        if(obj.objType == "Wood")
            resourceController.wood -= obj.cost;

        if(obj.objType == "Stone")
            resourceController.stone -= obj.cost;

        audioManager.PlaySound("Build");

        //Debug.Log("Placed " + objToPlace + " at position " + go.transform.position);
    }
}
