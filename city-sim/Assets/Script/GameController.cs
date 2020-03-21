﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public bool rtsMode = true;
    public Camera rtsCam, fpsCam;
    public Transform player;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        } else {
            Destroy(gameObject);
        }

        rtsMode = true;
        fpsCam.GetComponent<AudioListener>().enabled = false;
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
            Cursor.lockState = CursorLockMode.None;

            rtsCam.GetComponent<Camera>().enabled = true;

            fpsCam.GetComponent<Camera>().enabled = false;

            rtsCam.GetComponent<AudioListener>().enabled = true;

            fpsCam.GetComponent<AudioListener>().enabled = false;

            player.GetComponent<PlayerMovement>().enabled = false;

            return;
        }

        Cursor.lockState = CursorLockMode.Locked;

        rtsCam.GetComponent<Camera>().enabled = false;

        fpsCam.GetComponent<Camera>().enabled = true;

        rtsCam.GetComponent<AudioListener>().enabled = false;

        fpsCam.GetComponent<AudioListener>().enabled = true;

        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
