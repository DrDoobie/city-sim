using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBuilding : MonoBehaviour
{
    public bool isBuildingFence;
    public GameObject postObj, wallObj;

    GhostPointer ghostPointer;
    GameObject lastPost;

    void Awake()
    {
        ghostPointer = GetComponent<GhostPointer>();
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

        Vector3 startPos = ghostPointer.GetWorldPoint();

        startPos = ghostPointer.SnapPosition(startPos);

        GameObject startPost = Instantiate(postObj, startPos, Quaternion.identity);

        startPost.transform.position = new Vector3(startPos.x, startPos.y + 0.3f, startPos.z);

        lastPost = startPost;
    }

    void SetFence()
    {
        isBuildingFence = false;
    }

    void UpdateFence()
    {
        Vector3 current = ghostPointer.GetWorldPoint();

        current = ghostPointer.SnapPosition(current);

        current = new Vector3(current.x, current.y + 0.3f, current.z);

        if(!current.Equals(lastPost.transform.position))
        {
            CreateFenceSegment(current);
        }
    }

    void CreateFenceSegment(Vector3 current)
    {
        GameObject newPost = Instantiate(postObj, current, Quaternion.identity);

        Vector3 middle = Vector3.Lerp(newPost.transform.position, lastPost.transform.position, 0.5f);

        GameObject newWall = Instantiate(wallObj, middle, Quaternion.identity);

        newWall.transform.LookAt(lastPost.transform);

        lastPost = newPost;
    }
}
