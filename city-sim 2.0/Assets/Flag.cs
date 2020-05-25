using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{
    bool active;
    GameObject inputFieldGO;
    InputField inputField;

    void Start()
    {
        inputFieldGO = GameController.Instance.inputFieldGO;
        inputField = GameController.Instance.inputField;
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
            inputFieldGO.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Return))
            {
                active = false;
            }

            return;
        }

        inputFieldGO.SetActive(false);
    }

    public void UseFlag()
    {
        active = true;
    }
}
