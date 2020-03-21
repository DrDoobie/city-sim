using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool rtsMode;
    public Camera rtsCam, fpsCam;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ModeController();
    }

    private void ModeController()
    {
        if (Input.GetButtonDown("ModeSwitch"))
        {
            rtsMode = !rtsMode;
        }

        if (rtsMode)
        {
            rtsCam.GetComponent<Camera>().enabled = true;

            fpsCam.GetComponent<Camera>().enabled = false;

            player.GetComponent<PlayerMovement>().enabled = false;

            return;
        }

        rtsCam.GetComponent<Camera>().enabled = false;
        
        fpsCam.GetComponent<Camera>().enabled = true;

        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
