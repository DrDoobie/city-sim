using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public bool rtsMode = true, buildMode = false;
    public Camera rtsCam, fpsCam;
    public Transform player;
    public Text infoText;
    public Text displayText;
    public GameObject focusButton, crosshair;
    public BuildingSystem buildingSystem;
    public SelectionController selectionController;
    public ResourceController resourceController;

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

    void Update()
    {
        ModeController();
        Crosshair();
    }

    void ModeController()
    {
        if(Input.GetButtonDown("ModeSwitch"))
        {
            if(rtsMode && (selectionController.selectedObject != null))
            {
                selectionController.DeselectObject();
            }

            rtsMode = !rtsMode;
        }

        if(Input.GetButtonDown("BuildMode"))
        {
            buildMode = !buildMode;
        }

        if(rtsMode)
        {
            Cursor.lockState = CursorLockMode.None;

            rtsCam.GetComponent<Camera>().enabled = true;

            fpsCam.GetComponent<Camera>().enabled = false;

            rtsCam.GetComponent<AudioListener>().enabled = true;

            fpsCam.GetComponent<AudioListener>().enabled = false;

            return;
        }

        Cursor.lockState = CursorLockMode.Locked;

        rtsCam.GetComponent<Camera>().enabled = false;

        fpsCam.GetComponent<Camera>().enabled = true;

        rtsCam.GetComponent<AudioListener>().enabled = false;

        fpsCam.GetComponent<AudioListener>().enabled = true;
    }

    void Crosshair()
    {
        if(rtsMode)
        {
            focusButton.SetActive(true);

            crosshair.SetActive(false);

            return;
        }

        focusButton.SetActive(false);

        crosshair.SetActive(true);
    }
}
