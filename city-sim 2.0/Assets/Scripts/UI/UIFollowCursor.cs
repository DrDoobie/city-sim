using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowCursor : MonoBehaviour
{
    public Vector2 textOffset;

    void Update()
    {
        Vector2 cursorPos = Input.mousePosition;

        transform.position = new Vector2(cursorPos.x, cursorPos.y) + textOffset;
    }
}
