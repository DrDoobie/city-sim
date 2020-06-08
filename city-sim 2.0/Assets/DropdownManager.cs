using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            gameObject.SetActive(false);
        }
    }

    public void HandleInputData(int val)
    {
        if(val == 0)
        {
            Debug.Log("0");

            gameObject.SetActive(false);
        }

        if(val == 1)
        {
            Debug.Log("1");

            gameObject.SetActive(false);
        }
    }

    public void MoveToCursor()
    {
        Vector2 cursorPos = Input.mousePosition;

        GameController.Instance.dropDown.transform.position = new Vector2(cursorPos.x, cursorPos.y);
    }
}
