using UnityEngine;
using System.Collections;
 
public class RTSBuildingSystem : MonoBehaviour
{
    public bool buildMode;
    public float rotateSpeed;
    public Camera cam;
    public LayerMask mask;

    [Header("Ghost Object")]
    public bool canPlace;
    public Transform ghostObj;
    public Material[] ghostMaterials;
    public GameObject ghostObjectContainer;

    float lastPosX,lastPosY,lastPosZ;
    GameObject objToPlace;
    Vector3 mousePos;

    void Update()
    {
        if(Input.GetButtonDown("Build Mode"))
        {
            buildMode = !buildMode;

            ghostObj.GetComponentInChildren<GhostObject>().collisions = 0;
        }

        if(buildMode)
        {
            ghostObjectContainer.SetActive(true);

            BuildMode();

            return;
        }

        ghostObjectContainer.SetActive(false);
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
