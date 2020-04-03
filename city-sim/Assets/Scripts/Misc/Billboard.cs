using UnityEngine;
using System.Collections;
 
public class Billboard : MonoBehaviour
{
    private Camera mainCam;
 
    private void Start () {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward,
        mainCam.transform.rotation * Vector3.up);
    }
}