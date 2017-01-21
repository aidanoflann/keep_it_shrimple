using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager {

    int _turnCounter = -1;
    int _turnOfNextWave;

    private IWaveBehaviour currentBehaviour;
    private List<IWaveBehaviour> allBehaviours = new List<IWaveBehaviour>();

    public WaveManager()
    {
        this.allBehaviours.Add(new RightWave());
        this.CalculateTurnOfNextWaveAndBehaviour();
    }

	public void ApplyWaveToBoard(Board board)
    {
        this._turnCounter++;
        if (this._turnCounter >= this._turnOfNextWave)
        {
            this.currentBehaviour.DoWave(board);
            this._turnCounter = -1;
            this.CalculateTurnOfNextWaveAndBehaviour();
        }
    }

    private void CalculateTurnOfNextWaveAndBehaviour()
    {
        this._turnOfNextWave = Random.Range(1, 3);
        this.currentBehaviour = this.allBehaviours[Random.Range(0, this.allBehaviours.Count)];
    }
}

public interface IWaveBehaviour
{
    void DoWave(Board board);
}

class RightWave : IWaveBehaviour
{
    public void DoWave(Board board)
    {
        // iterate through all positions on the board, and set its value to the value to the left
        // note: iterating right to left
        for(int x = board._pieceMapping.numXSquares -1; x > -1; x--)
        {
            for (int z = 0; z < board._pieceMapping.numZSquares; z++)
            {
                Piece currentPiece = board._pieces[x, z];
                if (currentPiece != null)
                {
                    if (x == board._pieceMapping.numXSquares - 1)
                    {
                        Object.Destroy(currentPiece.gameObject);
                    }
                    else
                    {
                        int[] position = new int[2] { x + 1, z };
                        Vector3 newPosition = board._pieceMapping.GetCoordinate(position);
                        currentPiece.Place(newPosition, false);
                    }
                }
            }
        }
    }
}
