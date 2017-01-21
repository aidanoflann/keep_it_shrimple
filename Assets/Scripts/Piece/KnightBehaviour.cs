using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class KnightBehaviour : PieceBehaviour
{
    public List<int[]> legalPositions(Board myBoard, Piece currentPiece, out List<Piece> candidateDeadPeople)
    {
        int[] start = currentPiece.startPosition();
        List<int[]> positions = new List<int[]>();
        candidateDeadPeople = new List<Piece>();

        for (int x = -1; x <= 1; x = x + 2) {
            for (int z = -2; z <= 2; z = z + 4)
            {
                int[] candidate = { start[0] + x, start[1] + z };
                if (!isValidSpace(myBoard, candidate[0], candidate[1]))
                    continue;
                Piece piecesSpace = myBoard._pieces[start[0] + x, start[1] + z];
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

        for (int x = -2; x <= 2; x = x + 4)
        {
            for (int z = -1; z <= 1; z = z + 2)
            {
                int[] candidate = { start[0] + x, start[1] + z };
                if (!isValidSpace(myBoard, candidate[0], candidate[1]))
                    continue;
                Piece piecesSpace = myBoard._pieces[start[0] + x, start[1] + z];
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

    private bool isValidSpace(Board myBoard, int x_candidate, int z_candidate) {
        return (x_candidate >= 0 && x_candidate < myBoard._pieceMapping.numXSquares) && (z_candidate >= 0 && z_candidate < myBoard._pieceMapping.numZSquares);
    }
}