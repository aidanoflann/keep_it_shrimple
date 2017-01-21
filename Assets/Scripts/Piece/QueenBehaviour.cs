using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class QueenBehabviour : PieceBehaviour
{
    public List<int[]> legalPositions(Board myBoard, Piece currentPiece, out List<Piece> candidateDeadPeople)
    {

        List<Piece> deados01 = new List<Piece>();
        List<Piece> deados02 = new List<Piece>();
        PieceBehaviour rook = new RookBehaviour();
        PieceBehaviour bishop = new BishopBehaviour();
        List<int[]> legalPositions1 = rook.legalPositions(myBoard, currentPiece, out deados01);
        List<int[]> legalPositions2 = bishop.legalPositions(myBoard, currentPiece, out deados02);
        deados01.AddRange(deados02);
        candidateDeadPeople = deados01;
        legalPositions1.AddRange(legalPositions2);
        return legalPositions1;
    }
}