using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static bool cursorLock;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor_Lock_Off();
        }
    }
    
    public static void Cursor_Lock_On(){
        Cursor.lockState = CursorLockMode.Locked;
        cursorLock = true;
    }

    public static void Cursor_Lock_Off(){
        Cursor.lockState = CursorLockMode.None;
        cursorLock = false;
    }
}
