using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public interface PieceBehaviour
{
    List<int[]> legalPositions(Board myBoard, Piece currentPiece, out List<Piece> candidateDeadPeople);
}
