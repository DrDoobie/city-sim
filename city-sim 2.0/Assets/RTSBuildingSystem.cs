using UnityEngine;
using System.Collections;
 
public class RTSBuildingSystem : MonoBehaviour
{
    public enum CursorState{Building,Rotating}
 
    public CursorState state = CursorState.Building;
    public Camera cam;
    public Transform ObjToMove;
    public GameObject ObjToPlace;
    public LayerMask mask;
    GameObject builtObject;
    float LastPosX,LastPosY,LastPosZ;
    Vector3 mousePos;
   
    // Update is called once per frame
    void Update ()
    {
        mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
       
        if(Physics.Raycast(ray, out hit,Mathf.Infinity,mask))
        {
            float PosX = hit.point.x;
            float PosY = hit.point.y;
            float PosZ = hit.point.z;
 
            if(PosX != LastPosX || PosY != LastPosY || PosZ != LastPosZ)
            {
                LastPosX = PosX;
                LastPosY = PosY;
                LastPosZ = PosZ;
                ObjToMove.position = new Vector3(LastPosX,LastPosY+.5f,LastPosZ);
            }
            if(Input.GetMouseButton(0))
            {
                if(state == CursorState.Building)
                {
                    GameObject building = (GameObject)Instantiate(ObjToPlace,ObjToMove.position,Quaternion.identity);
                    builtObject = building;
                    state = CursorState.Rotating;
                }
                if(state == CursorState.Rotating)
                    builtObject.transform.LookAt(new Vector3(LastPosX,builtObject.transform.position.y,LastPosZ));
            }
            if(Input.GetMouseButtonUp(0))
            {
                builtObject = null;
                state = CursorState.Building;
            }
        }
    }
}