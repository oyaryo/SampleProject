using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/*
作品を簡易的にデータベースとして簡易的に管理するためのもの
*/
[Serializable]
public class Piece
{
    public enum PieceID
    {
        Piece001,
        Piece002,
        Piece003,
        Piece004,
        Piece005,
        Piece006,
        Piece007,
        Piece008,
        Piece009,
        Piece010,
        Piece011,
        Piece012,
        Piece013,
        Piece014,
        Piece015,
        Piece016,
        Piece017,
        Piece018,
        Piece019,
        Piece020,
    }

    public PieceID pieceID;
    public string pieceTitle;
    public string pieceAuther;
    [Multiline(5)]　public string textDiscription;
    public Sprite pieceImage;
    public AudioClip voiceDiscription;
    public VideoClip videoDiscription;

    public Piece(PieceID pieceID, string pieceTitle, string pieceAuther, string textDiscription, Sprite pieceImage, AudioClip voiceDiscription, VideoClip videoDiscription)
    {
        this.pieceID = pieceID; // 作品を取ります際の基本となるID
        this.pieceTitle = pieceTitle; // 作品名
        this.pieceAuther = pieceAuther; // 作者名
        this.textDiscription = textDiscription; // 作品の説明文 
        this.pieceImage = pieceImage; // 作品の画像
        this.voiceDiscription = voiceDiscription; // 作品の音声解説ファイル
        this.videoDiscription = videoDiscription; // 作品の動画解説ファイル
    }
}
