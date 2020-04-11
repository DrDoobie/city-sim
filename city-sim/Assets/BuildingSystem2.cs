using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem2 : MonoBehaviour
{
    public Camera rtsCam, fpsCam;

    [Header("Build Ghost")]
    public GameObject buildGhost;
    int lastPosGhostX, lastPosGhostY, lastPosGhostZ;

    Vector3 mousePosition;

    void Update()
    {
        if(GameController.Instance.buildMode)
        {
            BuildController();
        }
    }

    void BuildController()
    {
        RTSBuild();
    }

    void RTSBuild()
    {
        BuildGhost();
    }

    void BuildGhost()
    {
        mousePosition = Input.mousePosition;

        Ray ray = rtsCam.ScreenPointToRay(mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            //buildGhost.transform.position = hit.point;

            //Grid movement
            int posX = (int)Mathf.Round(hit.point.x);
            int posZ = (int)Mathf.Round(hit.point.z);

            if (posX != lastPosGhostX || posZ != lastPosGhostZ)
            {
                lastPosGhostX = posX;
                lastPosGhostZ = posZ;

                buildGhost.transform.position = new Vector3(posX, 0.0f, posZ);
            }
        }
    }
}
