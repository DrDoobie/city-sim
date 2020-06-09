using System.Collections;
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
        if(active)
        {
            tribeInfo.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Return))
            {
                TribeDeclaration();
                CloseFlag();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                if(!tribeDeclared)
                    inputField.text = null;

                CloseFlag();
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
        active = true;
        GameController.Instance.playerUsingUI = true;
    }

    void CloseFlag()
    {
        active = false;
        GameController.Instance.playerUsingUI = false;
    }
}
