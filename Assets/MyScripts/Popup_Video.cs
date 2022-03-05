using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Video;
using UnityEngine.UI;

public class Popup_Video: MonoBehaviour
{
    public GameController gameManager;
    public RawImage rawImage;
    Animator animator;

    void Start()
    {
        // SetVideoClip(gameManager.pieceID);
        this.animator = GetComponent<Animator>();
    }
    
    public void Popup_Open(){
        Lock_Off();
        Debug.Log("timeScale: " + Time.timeScale);
        Debug.Log(animator);
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

    // ビデオ再生用のポップアップにあるrawImageにクリップをセットする
    void SetVideoClip(string pieceID){
        Piece piece = PieceGenerator.instance.Spawn(pieceID);

        VideoPlayer videoPlayer = rawImage.GetComponent<VideoPlayer>();
        videoPlayer.clip = piece.videoDiscription;
    }
}
