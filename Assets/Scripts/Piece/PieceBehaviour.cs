using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


interface PieceBehaviour
{
    List<int[]> legalPositions( Board myBoard, Piece currentPiece, out List<Piece> candidateDeadPeople);
}

public class RookBehaviour : PieceBehaviour
{
    public List<int[]> legalPositions( Board myBoard, Piece currentPiece, out List<Piece> candidateDeadPeople) {

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
        for (int x_val = start[0] + 1; x_val < myBoard._pieceMapping.numXSquares; x_val++) {
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

    public List<int[]> legalPositions_depricated(Board myBoard, Piece currentPiece)
    {
        List<int[]> positions = new List<int[]>();
        //entities that exist on the game board, other pieces
        List<int[]> entities = new List<int[]>();
        //List<int[]> tempPositions = new List<int[]>();

        //add startposition as invalid
        int[] start = currentPiece.startPosition();


        for (int i = 0; i < myBoard._pieceMapping.numXSquares; i++)
        {
            bool countX = true; bool countZ = true;
            int[] z_Candidate = { i, start[1] };
            int[] x_Candidate = { start[0], i };


            if (myBoard._pieces[i, start[1]] == null && countX)
                positions.Add(z_Candidate);
            else if (myBoard._pieces[i, start[1]].colour != currentPiece.colour && countX)
            {
                positions.Add(z_Candidate); entities.Add(z_Candidate);
            }
            else
                entities.Add(z_Candidate);


            if (myBoard._pieces[start[0], i] == null && countZ)
                positions.Add(x_Candidate);
            else if (myBoard._pieces[start[0], i].colour != currentPiece.colour)
            {
                positions.Add(x_Candidate); entities.Add(x_Candidate);
            }
            else
                entities.Add(x_Candidate);


        }


        return positions;
    }
}