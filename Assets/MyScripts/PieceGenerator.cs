using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PieceGenerator : MonoBehaviour
{
    [SerializeField] PieceListEntity pieceListEntity;

    public static PieceGenerator instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public Piece Spawn(string pieceID)
    {
        foreach(Piece piece in pieceListEntity.pieceList)
        {
            if (piece.pieceID.ToString() == pieceID)
            {
                return new Piece(piece.pieceID, piece.pieceTitle, piece.pieceAuther, piece.textDiscription, piece.pieceImage, piece.voiceDiscription, piece.videoDiscription);
            }
        }
        return null;
    }

    public string GetPieceTitle(Piece.PieceID pieceID)
    {
        foreach(Piece piece in pieceListEntity.pieceList)
        {
            if(piece.pieceID == pieceID)
            {
                return piece.pieceTitle;
            }
        }
        return null;
    }

    public string GetPieceDiscription(Piece.PieceID pieceID)
    {
        foreach(Piece piece in pieceListEntity.pieceList)
        {
            if(piece.pieceID == pieceID)
            {
                return piece.textDiscription;
            }
        }
        return null;
    }

    public Sprite GetPieceImage(Piece.PieceID pieceID)
    {
        foreach(Piece piece in pieceListEntity.pieceList)
        {
            if(piece.pieceID == pieceID)
            {
                return piece.pieceImage;
            }
        }
        return null;
    }
}
