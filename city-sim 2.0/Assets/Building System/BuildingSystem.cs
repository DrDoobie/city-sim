using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class BuildingSystem : MonoBehaviour
{
    public bool buildMode = false;
    public int selectedObject = 0;
    public Camera rtsCam, fpsCam;
    public GameObject placementParticleEffect;
    public Text objectInfo;
    public LayerMask layerMask;

    [Header("Ghost Object")]
    public bool canPlace;
    public float rotationSpeed = 10.0f;
    public Transform ghostObjectContainer;
    public Material[] ghostMaterials;

    float lastPosX,lastPosY,lastPosZ;
    Camera activeCam;
    Transform ghostObj;

    void Start()
    {
        SelectObject();
    }

    void Update()
    {
        if(GameController.Instance.playerUsingUI)
            return;
        
        SwitchObject();

        if(Input.GetButtonDown("Build Mode"))
        {
            buildMode = !buildMode;
        }

        if(buildMode)
        {
            BuildMode();
            UIController();

            ghostObj.GetComponentInChildren<MeshRenderer>().enabled = true;

            return;
        }

        objectInfo.text = "";
        ghostObj.GetComponentInChildren<MeshRenderer>().enabled = false;
    }


    void SwitchObject()
    {
        int previousSelectedObject = selectedObject;

        if(Input.GetAxis("Mouse ScrollWheel") != 0.0f)
        {
            ResetObjectCollision();
        }

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
        if(GameController.Instance.rtsMode)
        {
            activeCam = rtsCam;

        } else{
            activeCam = fpsCam;
        }

        Ray ray = activeCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            MoveObject(hit);
            RotateObject();

            if(Input.GetButtonDown("Fire1"))
            {
                if(canPlace)
                {
                    PlaceObject();

                    return;
                }

                Debug.Log("Couldn't place object!");
            }
        }
    }

    void UIController()
    {
        Vector2 cursorPos = Input.mousePosition;

        objectInfo.transform.position = new Vector2(cursorPos.x, cursorPos.y);

        if(ghostObj.GetComponentInChildren<GhostObject>().buildingObject != null)
        {
            objectInfo.text = ghostObj.GetComponentInChildren<GhostObject>().buildingObject.objectInfo;

        } else {
            Debug.Log("Error: Couldn't find object info");
            objectInfo.text = "";
        }
    }

    void MoveObject(RaycastHit hit)
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
    }

    void RotateObject()
    {
        if(Input.GetKey(KeyCode.E))
        {
            ghostObj.transform.Rotate(Vector3.up, (-rotationSpeed * 10.0f) * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.Q))
        {
            ghostObj.transform.Rotate(Vector3.up, (rotationSpeed * 10.0f) * Time.deltaTime);
        }
    }

    void PlaceObject()
    {
        GameObject obj = (GameObject)Instantiate(ghostObj.GetComponentInChildren<GhostObject>().prefab, 
        ghostObj.position, ghostObj.rotation);

        //Particle effects
        if(placementParticleEffect != null)
        {
            GameObject particles = Instantiate(placementParticleEffect, ghostObj.position, Quaternion.identity);
            Destroy(particles, 2.0f);
        }
    }

    void ResetObjectCollision()
    {
        ghostObj.GetComponentInChildren<GhostObject>().collisions = 0;
    }
}
