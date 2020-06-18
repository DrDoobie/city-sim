using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            CloseMenu();
        }
    }

    public void HandleInputData(int val)
    {
        if(val == 0)
        {
            Debug.Log("0");

            CloseMenu();
        }

        if(val == 1)
        {
            Debug.Log("1");

            CloseMenu();
        }
    }

    public void MoveToCursor()
    {
        Vector2 cursorPos = Input.mousePosition;

        GameController.Instance.dropDown.transform.position = new Vector2(cursorPos.x, cursorPos.y);
    }

    void CloseMenu()
    {
        gameObject.SetActive(false);

        GameController.Instance.playerUsingUI = false;
    }
}
