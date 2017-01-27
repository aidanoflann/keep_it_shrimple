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
        if (currentPiece.colour == PieceColour.BLACK)
            modifier = -1;

        //check square immediately infront
        candidate[0] = start[0];
        candidate[1] = start[1]+(modifier);
        //if candidate is not in bounds, move on. . .
        if (isInBounds(myBoard, candidate[0], candidate[1]))
        {
            Piece pieceSpace = myBoard._pieces[candidate[0], candidate[1]];
            //if it is between 0<=candidate[i]<8 AND the space is unoccupied it is a valid position
            if (pieceSpace == null)
            {
                positions.Add(new int[] { candidate[0], candidate[1] });
                frontSquareOccupied = false;
            }
        }
        //check if you can go two spaces ahead
        candidate[0] = start[0];
        candidate[1] = start[1] + (modifier * 2);
        if (currentPiece.isFirstMove && !frontSquareOccupied && isInBounds(myBoard, candidate[0], candidate[1])) {
            Piece pieceSpace = myBoard._pieces[candidate[0], candidate[1]];
            if (pieceSpace == null)
            {
                positions.Add(new int[] { candidate[0], candidate[1] });
            }
        }
        //check if there are any pieces you can capture
        candidate[0] = start[0] + 1;
        candidate[1] = start[1] + modifier;
        if (isInBounds(myBoard, candidate[0], candidate[1]))
        {
            Piece pieceSpace = myBoard._pieces[candidate[0], candidate[1]];
            //if it is between 0<=candidate[i]<8 AND the space is unoccupied it is a valid position
            if (pieceSpace != null)
            {
                positions.Add(new int[] { candidate[0], candidate[1] });
                candidateDeadPeople.Add(pieceSpace);
            }
        }
        candidate[0] = start[0] - 1;
        candidate[1] = start[1] + modifier;
        if (isInBounds(myBoard, candidate[0], candidate[1]))
        {
            Piece pieceSpace = myBoard._pieces[candidate[0], candidate[1]];
            //if it is between 0<=candidate[i]<8 AND the space is unoccupied it is a valid position
            if (pieceSpace != null)
            {
                positions.Add(new int[] { candidate[0], candidate[1] });
                candidateDeadPeople.Add(pieceSpace);
            }
        }
        return positions;
    }

    private bool isInBounds(Board myBoard, int x_candidate, int z_candidate)
    {
        return (x_candidate >= 0 && x_candidate < myBoard._pieceMapping.numXSquares) && (z_candidate >= 0 && z_candidate < myBoard._pieceMapping.numZSquares);
    }

}
