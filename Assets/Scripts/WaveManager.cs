using UnityEngine;
using System.Collections;

public class WaveManager {

    int _turnCounter = -1;
    int _turnOfNextWave;

    public WaveManager()
    {
        this.CalculateTurnOfNextWave();
    }

	public void ApplyWaveToBoard(Board board)
    {
        this._turnCounter++;
        if (this._turnCounter >= this._turnOfNextWave)
        {
            // Do tha wave!
            this._turnCounter = -1;
            this.CalculateTurnOfNextWave();
        }
    }

    private void CalculateTurnOfNextWave()
    {
        this._turnOfNextWave = Random.Range(1, 4);
    }
}
