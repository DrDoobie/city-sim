﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{
    [SerializeField]
    bool active, tribeDeclared = false;

    GameObject tribeInfo;
    InputField inputField;

    void Awake()
    {
        tribeInfo = GameObject.FindWithTag("TribeInfoGO");
        inputField = GameObject.FindWithTag("TribeNameInput").GetComponent<InputField>();
    }

    void Update()
    {
        if(!GameController.Instance.rtsMode)
        {
            return;
        }

        Controller();
    }

    void Controller()
    {
        if(GameController.Instance.playerUsingUI)
        {
            tribeInfo.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Return))
            {
                TribeDeclaration();

                GameController.Instance.playerUsingUI = false;
            }

            if(Input.GetButtonDown("Fire2"))
            {
                if(!tribeDeclared)
                    inputField.text = null;

                GameController.Instance.playerUsingUI = false;
            }

            return;
        }

        tribeInfo.SetActive(false);
    }

    void TribeDeclaration()
    {
        if(!tribeDeclared)
        {
            if(inputField.text.Length > 0)
            {
                //inputField.interactable = false;
                tribeDeclared = true;
            }
        }
    }

    public void UseFlag()
    {
        GameController.Instance.playerUsingUI = true;
    }
}