using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public bool playerUsingUI = false;
    public BuildingSystem buildingSystem;
    public RTSControls rtsControls;

    [Header("UI")]
    public Text modeText;
    public GameObject dropDown;

    [Header("Mode Controller")]
    public bool rtsMode;
    public GameObject rtsCam, fpsCam;
    public PlayerMotor playerMotor;
    public CharacterController playerController;
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
        if(buildingSystem.buildMode)
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
                //rtsBuildingSystem.buildMode = false; //Disabling shit for a smoother experience, ugly code though
                //rtsBuildingSystem.ghostObjectContainer.gameObject.SetActive(false);

            } else{
                rtsControls.playerDestination = playerAgent.transform.position; //Reset player destination
            }

            rtsMode = !rtsMode;
        }

        if(rtsMode)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            rtsCam.SetActive(true);
            fpsCam.SetActive(false);

            if(playerMotor.isGrounded)
            {
                playerMotor.enabled = false;

                playerAgent.enabled = true;
            }

            //playerController.enabled = false;

            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rtsCam.SetActive(false);
        fpsCam.SetActive(true);

        playerMotor.enabled = true;
        //playerController.enabled = true;

        playerAgent.enabled = false;
    }
}
