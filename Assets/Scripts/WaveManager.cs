using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WaveDirection
{
    NONE = 0,
    UP = 1,
    DOWN = 2,
    RIGHT = 3,
    LEFT = 4
}

public class WaveManager {

    int _turnCounter = -1;
    int _turnOfNextWave;
    int _turnsSinceStart = -1;
    private WaveIndicator _waveIndicator;
    private WaveDirection _waveDirection;

    private IWaveBehaviour currentBehaviour;
    private List<IWaveBehaviour> allBehaviours = new List<IWaveBehaviour>();
    private List<IWaveBehaviour> safeBehaviours = new List<IWaveBehaviour>();
    private Animator _waveAnimator;
    private AnimatorStateInfo _waveAnimatorStateInfo;
    private bool waveHasHappened;

    public WaveManager()
    {
        this.allBehaviours.Add(new UpWave());
        this.allBehaviours.Add(new DownWave());
        this.allBehaviours.Add(new LeftWave());
        this.allBehaviours.Add(new RightWave());
        this.safeBehaviours.Add(new LeftWave());
        this.safeBehaviours.Add(new RightWave());

        this.CalculateTurnOfNextWaveAndBehaviour();
        this._waveAnimator = GameObject.Find("wave").GetComponent<Animator>();
        this._waveIndicator = GameObject.FindObjectOfType<WaveIndicator>();
        this._waveAnimatorStateInfo = this._waveAnimator.GetCurrentAnimatorStateInfo(0);
        this.waveHasHappened = false;
    }

    public void Tick(Board board)
    {
        if (this._waveAnimator != null)
        {
            this._waveAnimatorStateInfo = this._waveAnimator.GetCurrentAnimatorStateInfo(0);

            if (this._waveAnimatorStateInfo.IsTag("Waving") &&
                this._waveAnimatorStateInfo.normalizedTime >= 1f &&
                !this.waveHasHappened)
            {
                waveHasHappened = true;
                this.ApplyWaveToBoard(board);
                _waveAnimator.SetTrigger("EndWave");

            }
            else if (this._waveAnimatorStateInfo.IsName("Idle"))
            {
                waveHasHappened = false;
            }
        }
    }

    public void TriggerWave()
    {
        switch (this._waveDirection)
        {
            case (WaveDirection.LEFT):
                _waveAnimator.SetTrigger("StartWaveLeft");
                break;

            case (WaveDirection.UP):
                _waveAnimator.SetTrigger("StartWaveUp");
                break;

            case (WaveDirection.DOWN):
                _waveAnimator.SetTrigger("StartWaveDown");
                break;

            case (WaveDirection.RIGHT):
                _waveAnimator.SetTrigger("StartWaveRight");
                break;
        }
        
    }

    public bool IsAWaveDue
    {
        get
        {
            // note this property has a side effect lol jams
            this._turnCounter++;
            this._turnsSinceStart++;
            if (this._turnCounter + 2 >= this._turnOfNextWave)
            {
                this._waveIndicator.Indicate(this._waveDirection);
            }
            return this._turnCounter >= this._turnOfNextWave;
        }
    }

	public void ApplyWaveToBoard(Board board)
    {
        this.currentBehaviour.DoWave(board);
        this._waveIndicator.Hide();
        this._turnCounter = -1;
        this.CalculateTurnOfNextWaveAndBehaviour();
    }

    private void CalculateTurnOfNextWaveAndBehaviour()
    {
        this._turnOfNextWave = Random.Range(3, 4);
        if (this._turnsSinceStart <= 14)
        {
            this.currentBehaviour = this.safeBehaviours[Random.Range(0, this.safeBehaviours.Count)];
        }
        else
        {
            this.currentBehaviour = this.allBehaviours[Random.Range(0, this.allBehaviours.Count)];
        }
        this._waveDirection = this.currentBehaviour.GetDirection();
    }
}

public interface IWaveBehaviour
{
    void DoWave(Board board);
    WaveDirection GetDirection();
}

class LeftWave : IWaveBehaviour
{
    public WaveDirection GetDirection()
    {
        return WaveDirection.LEFT;
    }

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
                        currentPiece.Destroy();
                    }
                    else
                    {
                        int[] position = new int[2] { x + 1, z };
                        Vector3 newPosition = board._pieceMapping.GetCoordinate(position);
                        currentPiece.StartPlace(newPosition, false, true);
                    }
                }
            }
        }
    }
}

class RightWave : IWaveBehaviour
{
    public WaveDirection GetDirection()
    {
        return WaveDirection.RIGHT;
    }

    public void DoWave(Board board)
    {
        // iterate through all positions on the board, and set its value to the value to the left
        // note: iterating left to right
        for (int x = 0; x < board._pieceMapping.numXSquares; x++)
        {
            for (int z = 0; z < board._pieceMapping.numZSquares; z++)
            {
                Piece currentPiece = board._pieces[x, z];
                if (currentPiece != null)
                {
                    if (x == 0)
                    {
                        currentPiece.Destroy();
                    }
                    else
                    {
                        int[] position = new int[2] { x - 1, z };
                        Vector3 newPosition = board._pieceMapping.GetCoordinate(position);
                        currentPiece.StartPlace(newPosition, false, true);
                    }
                }
            }
        }
    }
}


class UpWave : IWaveBehaviour
{
    public WaveDirection GetDirection()
    {
        return WaveDirection.UP;
    }

    public void DoWave(Board board)
    {
        // iterate through all positions on the board, and set its value to the value to the left
        // note: iterating top to bottom
        for (int x = 0; x < board._pieceMapping.numXSquares; x++)
        {
            for (int z = 0; z < board._pieceMapping.numZSquares; z++)
            {
                Piece currentPiece = board._pieces[x, z];
                if (currentPiece != null)
                {
                    if (z == 0)
                    {
                        currentPiece.Destroy();
                    }
                    else
                    {
                        int[] position = new int[2] { x , z - 1 };
                        Vector3 newPosition = board._pieceMapping.GetCoordinate(position);
                        currentPiece.StartPlace(newPosition, false, true);
                    }
                }
            }
        }
    }
}

class DownWave : IWaveBehaviour
{
    public WaveDirection GetDirection()
    {
        return WaveDirection.DOWN;
    }

    public void DoWave(Board board)
    {
        // iterate through all positions on the board, and set its value to the value to the left
        // note: iterating top to bottom
        for (int x = 0; x < board._pieceMapping.numXSquares; x++)
        {
            for (int z = board._pieceMapping.numZSquares - 1; z > -1; z--)
            {
                Piece currentPiece = board._pieces[x, z];
                if (currentPiece != null)
                {
                    if (z == board._pieceMapping.numZSquares - 1)
                    {
                        currentPiece.Destroy();
                    }
                    else
                    {
                        int[] position = new int[2] { x, z + 1 };
                        Vector3 newPosition = board._pieceMapping.GetCoordinate(position);
                        currentPiece.StartPlace(newPosition, false, true);
                    }
                }
            }
        }
    }
}
