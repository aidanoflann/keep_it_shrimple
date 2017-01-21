using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BishopBehaviour : PieceBehaviour
{
    public List<int[]> legalPositions(Board myBoard, Piece currentPiece, out List<Piece> candidateDeadPeople)
    {
        int[] start = currentPiece.startPosition();
        List<int[]> positions = new List<int[]>();
        candidateDeadPeople = new List<Piece>();
        int[] candidate = new int[] { -1, -1 };

        //four bools to see if we will continue calculating stuff. North-West etc.
        bool ne=true, nw = true, sw = true, se = true;
        for (int x = 1; x < 7; x++) {
            //This shit will be done four times for the four axis of movement
            
            //NE
            //set candidate and test if it's in bounds
            candidate[0] = start[0] + x;
            candidate[1] = start[1] + x;
            //check if it's in bounds
            ne = isInBounds(myBoard, candidate[0], candidate[1]);
            //If in bounds see if everything else is fine
            if (ne) {

                Piece piecesSpace = myBoard._pieces[candidate[0], candidate[1]];
                if (piecesSpace == null)
                    positions.Add(new int[] { candidate[0], candidate[1]});
                else if (piecesSpace.colour != currentPiece.colour)
                {
                    positions.Add(new int[] { candidate[0], candidate[1] });
                    candidateDeadPeople.Add(piecesSpace);
                    ne = false;
                }
                else
                    ne = false;
            }

            //SE
            candidate[0] = start[0] + x;
            candidate[1] = start[1] - x;
            //check if it's in bounds
            se = isInBounds(myBoard, candidate[0], candidate[1]);
            //If in bounds see if everything else is fine
            if (se)
            {

                Piece piecesSpace = myBoard._pieces[candidate[0], candidate[1]];
                if (piecesSpace == null)
                    positions.Add(new int[] { candidate[0], candidate[1] });
                else if (piecesSpace.colour != currentPiece.colour)
                {
                    positions.Add(new int[] { candidate[0], candidate[1] });
                    candidateDeadPeople.Add(piecesSpace);
                    se = false;
                }
                else
                    se = false;
            }

            //SW
            candidate[0] = start[0] - x;
            candidate[1] = start[1] - x;
            //check if it's in bounds
            sw = isInBounds(myBoard, candidate[0], candidate[1]);
            //If in bounds see if everything else is fine
            if (sw)
            {

                Piece piecesSpace = myBoard._pieces[candidate[0], candidate[1]];
                if (piecesSpace == null)
                    positions.Add(new int[] { candidate[0], candidate[1] });
                else if (piecesSpace.colour != currentPiece.colour)
                {
                    positions.Add(new int[] { candidate[0], candidate[1] });
                    candidateDeadPeople.Add(piecesSpace);
                    sw = false;
                }
                else
                    sw = false;
            }

            //NW
            candidate[0] = start[0] - x;
            candidate[1] = start[1] + x;
            //check if it's in bounds
            nw = isInBounds(myBoard, candidate[0], candidate[1]);
            //If in bounds see if everything else is fine
            if (nw)
            {

                Piece piecesSpace = myBoard._pieces[candidate[0], candidate[1]];
                if (piecesSpace == null)
                    positions.Add(new int[] { candidate[0], candidate[1] });
                else if (piecesSpace.colour != currentPiece.colour)
                {
                    positions.Add(new int[] { candidate[0], candidate[1] });
                    candidateDeadPeople.Add(piecesSpace);
                    nw = false;
                }
                else
                    nw = false;
            }

            //quit if everything is done now
            if (!ne && !nw && !sw && !se)
                break;
        }

        return positions;
    }

    private bool isInBounds(Board myBoard, int x_candidate, int z_candidate)
    {
        return (x_candidate >= 0 && x_candidate < myBoard._pieceMapping.numXSquares) && (z_candidate >= 0 && z_candidate < myBoard._pieceMapping.numZSquares);
    }
}