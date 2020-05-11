using UnityEngine;
using System.Collections;
 
public class RTSBuildingSystem : MonoBehaviour
{
    public enum CursorState{Building}
 
    public float rotateSpeed;
    public CursorState state = CursorState.Building;
    public Camera cam;
    public Transform ghostObj;
    public GameObject objToPlace;
    public LayerMask mask;

    float lastPosX,lastPosY,lastPosZ;
    GameObject builtObject;
    Vector3 mousePos;
   
    void Update()
    {
        BuildMode();
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

            if(Input.GetMouseButtonDown(0))
            {
                //Building
                if (state == CursorState.Building)
                {
                    GameObject building = (GameObject)Instantiate(objToPlace, ghostObj.position, ghostObj.rotation);

                    builtObject = building;

                    //state = CursorState.Rotating;
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                builtObject = null;

                state = CursorState.Building;
            }
        }
    }
}