using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{
    [Header("UI")]
    public GameObject inputFieldGO;
    public InputField inputField;

    [SerializeField]
    bool active, tribeDeclared = false;

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
            inputFieldGO.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Return))
            {
                TribeDeclaration();

                GameController.Instance.playerUsingUI = false;
            }

            return;
        }

        inputFieldGO.SetActive(false);
    }

    void TribeDeclaration()
    {
        if(!tribeDeclared)
        {
            if(inputField.text.Length > 0)
            {
                inputField.interactable = false;
                tribeDeclared = true;
            }
        }
    }

    public void UseFlag()
    {
        GameController.Instance.playerUsingUI = true;
    }
}
