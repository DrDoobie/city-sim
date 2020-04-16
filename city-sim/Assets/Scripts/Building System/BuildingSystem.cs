using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
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

    private void BuildController()
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

    private void PlaceObj()
    {
        ResourceStorage resourceStorage = objToPlace.GetComponent<ResourceStorage>();

        if(resourceStorage)
        {
            resourceStorage.AddToStorage();
        }

        //Determina resource needed
        ObjectInfo objInfo = objToPlace.GetComponent<ObjectInfo>();

        if(objInfo.obj.objType == "Wood")
            if(GameController.Instance.resourceController.wood >= objInfo.obj.cost)
            {
                if(ghostObj.GetComponent<GhostObject>().canPlace)
                {
                    GameObject go = Instantiate(objToPlace, ghostObj.transform.position, ghostObj.transform.rotation);
                
                    GameController.Instance.resourceController.wood -= objInfo.obj.cost;

                    FindObjectOfType<AudioManager>().PlaySound("Build");

                    //Debug.Log("Placed " + objToPlace + " at position " + go.transform.position);
                    return;
                }

                Debug.Log("Error: not enough space to place");
                return;
            }

        if(objInfo.obj.objType == "Stone")
            if(GameController.Instance.resourceController.stone >= objInfo.obj.cost)
            {
                if(ghostObj.GetComponent<GhostObject>().canPlace)
                {
                    GameObject go = Instantiate(objToPlace, ghostObj.transform.position, ghostObj.transform.rotation);
                    
                    GameController.Instance.resourceController.stone -= objInfo.obj.cost;

                    FindObjectOfType<AudioManager>().PlaySound("Build");

                    //Debug.Log("Placed " + objToPlace + " at position " + go.transform.position);
                    return;
                }
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

        ghostObj = rtsObjects[obj];

        GameObject go = Instantiate(ghostObj, lastPos, ghostObj.transform.rotation);

        go.AddComponent(typeof(GhostObject));
    
        ghostObj = go;
    }
}
