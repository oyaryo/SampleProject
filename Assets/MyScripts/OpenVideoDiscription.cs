using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenVideoDiscription : MonoBehaviour
{
    public PopupPieceDiscription popupPieceDiscription;
    public Popup_Video popup;
    string pieceID;

    public void OnClickBtn()
    {
        Debug.Log("ボタンは押されています");
        this.pieceID = popupPieceDiscription.pieceID; 
        // popup.Popup_Open(pieceID);
        popup.Popup_Open("Piece001");
    }
}
