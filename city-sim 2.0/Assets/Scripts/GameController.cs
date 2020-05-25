using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public bool buildMode = false, playerUsingUI = false;
    public RTSBuildingSystem rtsBuildingSystem;

    [Header("UI")]
    public Text modeText;

    [Header("Mode Controller")]
    public bool rtsMode;
    public GameObject rtsCam, fpsCam;
    public PlayerMotor playerMotor;
    public NavMeshAgent playerAgent;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        } else {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        UIController();

        if(playerUsingUI)
            return;

        ModeController();
    }

    void UIController()
    {
        if(rtsBuildingSystem.buildMode)
        {
            modeText.text = "Build Mode";

            return;
        }

        modeText.text = "";
    }

    void ModeController()
    {
        if(Input.GetButtonDown("Mode Switch"))
        {
            if(rtsMode)
            {
                rtsBuildingSystem.buildMode = false;

                rtsBuildingSystem.ghostObjectContainer.SetActive(false);
            }

            rtsMode = !rtsMode;
        }

        if(rtsMode)
        {
            Cursor.lockState = CursorLockMode.None;

            rtsCam.SetActive(true);
            fpsCam.SetActive(false);

            playerMotor.enabled = false;
            playerAgent.enabled = true;

            return;
        }

        Cursor.lockState = CursorLockMode.Locked;

        rtsCam.SetActive(false);
        fpsCam.SetActive(true);

        playerMotor.enabled = true;
        playerAgent.enabled = false;
    }
}
