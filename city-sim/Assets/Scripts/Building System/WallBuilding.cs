using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuilding : MonoBehaviour
{
    public bool isBuildingFence;
    public GameObject postObj, wallObj;
    public Camera cam;

    GhostObject ghostObject;
    GameObject lastPost;

    void Awake()
    {
        ghostObject = GetComponent<GhostObject>();
    }

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartFence();

        } else if(Input.GetMouseButtonUp(0)) {
            SetFence();

        } else {
            if(isBuildingFence)
            {
                UpdateFence();
            }
        }
    }

    void StartFence()
    {
        isBuildingFence = true;

        Vector3 startPos = GetWorldPoint();

        startPos = SnapPosition(startPos);

        GameObject startPost = Instantiate(postObj, startPos, Quaternion.identity);

        startPost.transform.position = new Vector3(startPos.x, startPos.y, startPos.z);

        lastPost = startPost;
    }

    void SetFence()
    {
        isBuildingFence = false;
    }

    void UpdateFence()
    {
        Vector3 current = GetWorldPoint();

        current = SnapPosition(current);

        current = new Vector3(current.x, current.y, current.z);

        if(!current.Equals(lastPost.transform.position))
        {
            CreateFenceSegment(current);
        }
    }

    void CreateFenceSegment(Vector3 current)
    {
        GameObject newPost = Instantiate(postObj, current, Quaternion.identity);

        float dist = Vector3.Distance(newPost.transform.position, lastPost.transform.position);

        if(dist > 1)
            return;

        Vector3 middle = Vector3.Lerp(newPost.transform.position, lastPost.transform.position, 0.5f);

        GameObject newWall = Instantiate(wallObj, middle, Quaternion.identity);

        newWall.transform.LookAt(lastPost.transform);

        lastPost = newPost;
    }

    public Vector3 GetWorldPoint()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    public Vector3 SnapPosition(Vector3 original)
    {
        Vector3 snapped;

        snapped.x = Mathf.Floor(original.x + 0.5f);
        snapped.y = Mathf.Floor(original.y + 0.5f);
        snapped.z = Mathf.Floor(original.z + 0.5f);

        return snapped;
    }
}