using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class PopupPieceDiscription : MonoBehaviour
{
    // PieceIDをオブジェクト間で共有するため
    public GameController gameManager;
    // 詳細画面表示後コントロールパネルを非表示にする為事前に取得する
    public GameObject controllerPanel;
    public string pieceID;
    public GameObject pieceTitle;
    public GameObject pieceAuther;
    public GameObject textDiscription;
    public GameObject pieceImage;
    public GameObject voiceDiscription;
    public VideoClip videoDiscription;

    AudioSource audioSource;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    public void Popup_Open(){
        pieceID = gameManager.pieceID;
        animator.SetTrigger("open");
        SetPieceDiscription(pieceID);

        // 詳細画面を表示した際コントローラーパネルを非表示にする
        controllerPanel.SetActive(false);
    }

    public void Popup_Close(){
        animator.SetTrigger("close");
        if(audioSource.isPlaying){
            audioSource.Stop();
        }

        // 詳細画面を閉じた後コントローラーパネルを表示する
        controllerPanel.SetActive(true);
    }

    void Lock_On(){
        Time.timeScale = 1.0f; 
        CursorManager.Cursor_Lock_On();
        Debug.Log(CursorManager.cursorLock);
    }

    void Lock_Off(){
        Time.timeScale = 0f;
        CursorManager.Cursor_Lock_Off();
        Debug.Log(CursorManager.cursorLock);
    }

    void SetPieceDiscription(string pieceID){
        // ポップアップ画面に表示したい作品の情報をインスタンスとして取得
        Piece piece = PieceGenerator.instance.Spawn(pieceID);

        pieceTitle.GetComponent<TextMeshProUGUI>().text = piece.pieceTitle;
        pieceAuther.GetComponent<TextMeshProUGUI>().text = piece.pieceAuther;
        textDiscription.GetComponent<Text>().text = piece.textDiscription;
        pieceImage.GetComponent<Image>().sprite = piece.pieceImage;
        voiceDiscription.GetComponent<VoicePlayer>().voiceSample = piece.voiceDiscription;
        videoDiscription = piece.videoDiscription;

        audioSource = voiceDiscription.GetComponent<AudioSource>();
    }
}