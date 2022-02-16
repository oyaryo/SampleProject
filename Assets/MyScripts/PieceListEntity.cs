using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PieceListEntity", menuName ="Create PieceListEntity")]
public class PieceListEntity : ScriptableObject
{
    public List<Piece> pieceList = new List<Piece>();
}
