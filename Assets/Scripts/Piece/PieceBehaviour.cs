using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


interface PieceBehaviour
{
    List<int[]> legalPositions( Board myBoard, Piece currentPiece);
}

public class RookBehaviour : PieceBehaviour
{
    public List<int[]> legalPositions( Board myBoard, Piece currentPiece) {
        List<int[]> positions = new List<int[]>();
        //List<int[]> tempPositions = new List<int[]>();

        //add startposition as invalid
        int[] start = currentPiece.startPosition();


        for (int i = 0; i < myBoard._pieceMapping.numXSquares; i++) {
            int[] z_Candidate = {i, start[1]};
            int[] x_Candidate = {start[0], i};
            if(myBoard._pieces[i, start[1]]==null)
                positions.Add(z_Candidate);
            if (myBoard._pieces[start[0], i]==null)
                positions.Add(x_Candidate);
        }

        return positions;
    }
}