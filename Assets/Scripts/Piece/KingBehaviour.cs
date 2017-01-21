using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class KingBehaviour : PieceBehaviour
{
    public List<int[]> legalPositions(Board myBoard, Piece currentPiece, out List<Piece> candidateDeadPeople)
    {
        int[] start = currentPiece.startPosition();
        List<int[]> positions = new List<int[]>();
        candidateDeadPeople = new List<Piece>();

        for (int x = -1; x < 2; x++)
        {
            int x_candidate = start[0] + x;
            if (x_candidate < 0 || x_candidate >= myBoard._pieceMapping.numXSquares)
                continue;
            for (int z = -1; z < 2; z++)
            {
                int z_candidate = start[1] + z;
                //test if its a valid square
                if ((x == 0 && z == 0) || (z_candidate < 0 || z_candidate >= myBoard._pieceMapping.numZSquares))
                    continue;
                //test for existing items in this space
                int[] candidate = { x_candidate, z_candidate };
                Piece piecesSpace = myBoard._pieces[x_candidate, z_candidate];
                if (piecesSpace == null)
                    positions.Add(candidate);
                else if (piecesSpace.colour != currentPiece.colour)
                {
                    positions.Add(candidate);
                    candidateDeadPeople.Add(piecesSpace);
                    continue;
                }
                else
                    continue;
            }
        }
        return positions;
    }
}