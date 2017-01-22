using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PrawnBehaviour : PieceBehaviour
{    

    public List<int[]> legalPositions(Board myBoard, Piece currentPiece, out List<Piece> candidateDeadPeople) {

        Boolean frontSquareOccupied = true;
        int[] start = currentPiece.startPosition();
        List<int[]> positions = new List<int[]>();
        candidateDeadPeople = new List<Piece>();
        int[] candidate = { -1, -1 };

        //White pieces will have a +ve z direction only
        //Black pieces will have a -ze z direction only
        int modifier = 1;
        if (currentPiece.colour == PieceColour.WHITE)
            modifier = -1;

        //check square immediately infront
        candidate[0] = start[0];
        candidate[1] = start[1]+(modifier);
        Piece pieceSpace = myBoard._pieces[candidate[0], candidate[1]];
        //if it is between 0<=candidate[i]<8 AND the space is unoccupied it is a valid position
        if ((candidate[1] < myBoard._pieceMapping.numZSquares && candidate[1] >= 0) && pieceSpace == null) {
            positions.Add(new int[] { candidate[0], candidate[1]});
            frontSquareOccupied = false;
        }
        //check if you can go two spaces ahead
        if (currentPiece.isFirstMove && !frontSquareOccupied) {
            candidate[0] = start[0];
            candidate[1] = start[1] + (modifier*2);
            pieceSpace = myBoard._pieces[candidate[0], candidate[1]];
            if ((candidate[1] < myBoard._pieceMapping.numZSquares && candidate[1] >= 0) && pieceSpace == null)      //redundant lel
            {
                positions.Add(new int[] { candidate[0], candidate[1] });
                frontSquareOccupied = false;
            }
        }
        //check if there are any pieces you can capture
        candidate[0] = start[0] + 1;
        candidate[1] = start[1] + modifier;
        pieceSpace = myBoard._pieces[candidate[0], candidate[1]];
        //if it is between 0<=candidate[i]<8 AND the space is unoccupied it is a valid position
        if ((pieceSpace != null) && ((candidate[0] < myBoard._pieceMapping.numXSquares && candidate[1] >= 0) && 
            (candidate[1] < myBoard._pieceMapping.numZSquares && candidate[1] >= 0)))
        {
            positions.Add(new int[] { candidate[0], candidate[1] });
            candidateDeadPeople.Add(pieceSpace);
        }
        candidate[0] = start[0] - 1;
        candidate[1] = start[1] + modifier;
        pieceSpace = myBoard._pieces[candidate[0], candidate[1]];
        //if it is between 0<=candidate[i]<8 AND the space is unoccupied it is a valid position
        if ((pieceSpace != null) && ( ( candidate[0] < myBoard._pieceMapping.numXSquares && candidate[1] >= 0 ) && 
            (candidate[1] < myBoard._pieceMapping.numZSquares && candidate[1] >= 0)))
        {
            positions.Add(new int[] { candidate[0], candidate[1] });
            candidateDeadPeople.Add(pieceSpace);
        }
        
        return positions;
    }
}
