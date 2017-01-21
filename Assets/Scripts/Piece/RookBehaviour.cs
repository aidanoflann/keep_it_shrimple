using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RookBehaviour : PieceBehaviour
{
    public List<int[]> legalPositions(Board myBoard, Piece currentPiece, out List<Piece> candidateDeadPeople)
    {

        int[] start = currentPiece.startPosition();
        List<int[]> positions = new List<int[]>();
        candidateDeadPeople = new List<Piece>();
        //Iterate through 4 possible directions accumulating legalPositions
        //x+ve

        for (int x_val = start[0] - 1; x_val >= 0; x_val--)
        {
            int[] candidate = { x_val, start[1] };
            Piece piecesSpace = myBoard._pieces[x_val, start[1]];
            if (piecesSpace == null)
                positions.Add(candidate);
            else if (piecesSpace.colour != currentPiece.colour)
            {
                positions.Add(candidate);
                candidateDeadPeople.Add(piecesSpace);
                break;
            }
            else
                break;
        }
        for (int x_val = start[0] + 1; x_val < myBoard._pieceMapping.numXSquares; x_val++)
        {
            int[] candidate = { x_val, start[1] };
            Piece piecesSpace = myBoard._pieces[x_val, start[1]];
            if (piecesSpace == null)
                positions.Add(candidate);
            else if (piecesSpace.colour != currentPiece.colour)
            {
                positions.Add(candidate);
                candidateDeadPeople.Add(piecesSpace);
                break;
            }
            else
                break;
        }


        for (int z_val = start[1] - 1; z_val >= 0; z_val--)
        {
            int[] candidate = { start[0], z_val };
            Piece piecesSpace = myBoard._pieces[start[0], z_val];
            if (piecesSpace == null)
                positions.Add(candidate);
            else if (piecesSpace.colour != currentPiece.colour)
            {
                positions.Add(candidate);
                candidateDeadPeople.Add(piecesSpace);
                break;
            }
            else
                break;
        }
        for (int z_val = start[1] + 1; z_val < myBoard._pieceMapping.numXSquares; z_val++)
        {
            int[] candidate = { start[0], z_val };
            Piece piecesSpace = myBoard._pieces[start[0], z_val];
            if (piecesSpace == null)
                positions.Add(candidate);
            else if (piecesSpace.colour != currentPiece.colour)
            {
                positions.Add(candidate);
                candidateDeadPeople.Add(piecesSpace);
                break;
            }
            else
                break;
        }

        return positions;
    }
}