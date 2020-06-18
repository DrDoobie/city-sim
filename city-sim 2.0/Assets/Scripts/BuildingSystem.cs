using UnityEngine;
using System.Collections;
 
public class BuildingSystem : MonoBehaviour
{
    /*public bool buildMode;
    public float rotateSpeed;
    public int selectedObject = 0;
    public LayerMask mask;

    [Header("Ghost Object")]
    public bool canPlace;
    public Material[] ghostMaterials;

    float lastPosX,lastPosY,lastPosZ;
    GameObject objToPlace;
    Camera cam;
    Vector3 mousePos;
    Transform ghostObj;*/
    public Transform ghostObjectContainer;

    void Start()
    {
        SelectObject();

        cam = GetComponent<RTSCamera>().rtsCam;
    }

    void Update()
    {
        if(GameController.Instance.playerUsingUI)
            return;
        
        SelectedObjectController();

        if(Input.GetButtonDown("Build Mode"))
        {
            ghostObj.GetComponentInChildren<GhostObject>().collisions = 0;//I think a bug might come from here

            buildMode = !buildMode;
        }

        if(buildMode)
        {
            BuildMode();

            ghostObj.GetComponentInChildren<MeshRenderer>().enabled = true;

            return;
        }

        ghostObj.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    void SelectedObjectController()
    {
        int previousSelectedObject = selectedObject;

        if(Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            if(selectedObject >= ghostObjectContainer.childCount - 1)
                selectedObject = 0;

            else
                selectedObject++;
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            if(selectedObject <= 0)
                selectedObject = ghostObjectContainer.childCount - 1;

            else
                selectedObject--;
        }

        if(previousSelectedObject != selectedObject)
        {
            SelectObject();
        }
    }

    void SelectObject() 
    {
        int i = 0;

        foreach(Transform obj in ghostObjectContainer)
        {
            if(i == selectedObject)
            {
                obj.gameObject.SetActive(true);

                ghostObj = obj;

            } else {
                obj.gameObject.SetActive(false);
            }

            i++;
        }
    }

    void BuildMode()
    {
        mousePos = Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(mousePos);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            //Recording mouse position
            float posX = hit.point.x;
            float posY = hit.point.y;
            float posZ = hit.point.z;

            if(posX != lastPosX || posY != lastPosY || posZ != lastPosZ)
            {
                lastPosX = posX;
                lastPosY = posY;
                lastPosZ = posZ;

                ghostObj.position = new Vector3(lastPosX, lastPosY + .5f, lastPosZ);
            }

            //Rotation
            if(Input.GetKey(KeyCode.E))
            {
                ghostObj.transform.Rotate(Vector3.up, (-rotateSpeed * 10.0f) * Time.deltaTime);
            }

            if(Input.GetKey(KeyCode.Q))
            {
                ghostObj.transform.Rotate(Vector3.up, (rotateSpeed * 10.0f) * Time.deltaTime);
            }

            if(Input.GetButtonDown("Fire1"))
            {
                //Build object
                if(canPlace)
                {
                    Build();
                }
            }
        }
    }

    void Build()
    {
        objToPlace = ghostObj.GetComponentInChildren<GhostObject>().prefab;

        GameObject building = (GameObject)Instantiate(objToPlace, ghostObj.position, ghostObj.rotation);
    }
}
