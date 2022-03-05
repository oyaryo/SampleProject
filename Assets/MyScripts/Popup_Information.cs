using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Popup_Information : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    public void Popup_Open(){
        animator.SetTrigger("open");
    }

    public void Popup_Close(){
        animator.SetTrigger("close");
    }

    void Lock_On(){
        if(Time.timeScale == 0f){
            Time.timeScale = 1.0f; 
        }
        // CursorManager.Cursor_Lock_On();
    }
    void Lock_Off(){
        Time.timeScale = 0f;
        // CursorManager.Cursor_Lock_Off();
    }
}
