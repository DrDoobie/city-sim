using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Mode Controller")]
    public bool rtsMode;
    public GameObject rtsCam, fpsCam;
    public PlayerMotor playerMotor;

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
        ModeController();
    }

    void ModeController()
    {
        if(Input.GetButtonDown("Mode Switch"))
        {
            rtsMode = !rtsMode;
        }

        if(rtsMode)
        {
            Cursor.lockState = CursorLockMode.None;

            rtsCam.SetActive(true);
            fpsCam.SetActive(false);

            playerMotor.enabled = false;

            return;
        }

        Cursor.lockState = CursorLockMode.Locked;

        rtsCam.SetActive(false);
        fpsCam.SetActive(true);

        playerMotor.enabled = true;
    }
}
