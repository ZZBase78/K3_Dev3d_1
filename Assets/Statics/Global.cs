using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static void SetCursor(bool onoff)
    {
        if (onoff)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
