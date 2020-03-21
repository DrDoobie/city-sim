using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public Transform ghostObj;
    public GameObject obj;

    int lastPosX, lastPosY, lastPosZ;
    Vector3 mousePos;

    // Update is called once per frame
    void Update()
    {
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

        if(Physics.Raycast(ray, out hit, 1000.0f))
        {
            int posX = (int)Mathf.Round(hit.point.x);
            int posZ = (int)Mathf.Round(hit.point.z);

            if(posX != lastPosX || posZ != lastPosZ)
            {
                lastPosX = posX;
                lastPosZ = posZ;

                ghostObj.position = new Vector3(posX, 0.0f, posZ);
            }

            if(Input.GetButtonDown("Fire1"))
            {
                Instantiate(obj, ghostObj.position, Quaternion.identity);
            }
        }
    }
}
