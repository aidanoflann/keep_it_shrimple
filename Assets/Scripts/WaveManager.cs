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
    private List<int> _dangerRows = new List<int>() { 1, 3, 4, 6 };

    private IWaveBehaviour currentBehaviour;
    private List<IWaveBehaviour> allBehaviours = new List<IWaveBehaviour>();
    private List<IWaveBehaviour> safeBehaviours = new List<IWaveBehaviour>();
    private Animator _waveAnimator;

    private GameObject _animatedWave;
    private SpriteRenderer _animatedWaveRenderer;
    private Animator _animatedWaveAnimator;

    private GameObject _animatedVertWave;
    private SpriteRenderer _animatedVertWaveRenderer;
    private Animator _animatedVertWaveAnimator;

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

        this._animatedWave = GameObject.Find("wave_horizontal");
        this._animatedWaveRenderer = this._animatedWave.GetComponent<SpriteRenderer>();
        this._animatedWaveRenderer.enabled = false;
        this._animatedWaveAnimator = this._animatedWave.GetComponent<Animator>();

        this._animatedVertWave = GameObject.Find("wave_vertical");
        this._animatedVertWaveRenderer = this._animatedVertWave.GetComponent<SpriteRenderer>();
        this._animatedVertWaveRenderer.enabled = false;
        this._animatedVertWaveAnimator = this._animatedVertWave.GetComponent<Animator>();

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

    public bool IsAnimationPlaying()
    {
        this._waveAnimatorStateInfo = this._waveAnimator.GetCurrentAnimatorStateInfo(0);
        return this._waveAnimatorStateInfo.IsTag("Waving") || this._waveAnimatorStateInfo.IsTag("Waved");
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
                if (this._waveDirection == WaveDirection.LEFT)
                {
                    this._animatedWave.transform.eulerAngles = new Vector3(90, 180, 0);
                    this._animatedWaveAnimator.SetTrigger("IndicateRightWave");
                    this._animatedWaveAnimator.SetBool("Indicating", true);
                    this._animatedWaveRenderer.enabled = true;
                }
                else if (this._waveDirection == WaveDirection.RIGHT)
                {
                    this._animatedWave.transform.eulerAngles = new Vector3(90, 0, 0);
                    this._animatedWaveAnimator.SetTrigger("IndicateLeftWave");
                    this._animatedWaveAnimator.SetBool("Indicating", true);
                    this._animatedWaveRenderer.enabled = true;
                }
                else if (this._waveDirection == WaveDirection.DOWN)
                {
                    this._animatedVertWave.transform.localScale = new Vector3(0.67f, -1, 1);
                    this._animatedVertWaveAnimator.SetTrigger("IndicateUpWave");
                    this._animatedVertWaveAnimator.SetBool("Indicating", true);
                    this._animatedVertWaveRenderer.enabled = true;
                }
                else if (this._waveDirection == WaveDirection.UP)
                {
                    this._animatedVertWave.transform.localScale = new Vector3(0.67f, 1, 1);
                    this._animatedVertWaveAnimator.SetTrigger("IndicateDownWave");
                    this._animatedVertWaveAnimator.SetBool("Indicating", true);
                    this._animatedVertWaveRenderer.enabled = true;
                }
            }
            return this._turnCounter >= this._turnOfNextWave;
        }
    }

	public void ApplyWaveToBoard(Board board)
    {
        this.currentBehaviour.DoWave(board, this._dangerRows);
        this._waveIndicator.Hide();
        this._animatedWaveRenderer.enabled = false;
        this._animatedVertWaveRenderer.enabled = false;
        this._turnCounter = -1;
        this.CalculateTurnOfNextWaveAndBehaviour();
        this._animatedWaveAnimator.SetBool("Indicating", false);
        this._animatedVertWaveAnimator.SetBool("Indicating", false);
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
    void DoWave(Board board, List<int> dangerRows);
    WaveDirection GetDirection();
}

class LeftWave : IWaveBehaviour
{
    public WaveDirection GetDirection()
    {
        return WaveDirection.LEFT;
    }

    public void DoWave(Board board, List<int> dangerRows)
    {
        // iterate through all positions on the board, and set its value to the value to the left
        // note: iterating right to left
        for(int x = board._pieceMapping.numXSquares -1; x > -1; x--)
        {
            for (int z = 0; z < board._pieceMapping.numZSquares; z++)
            {
                if (dangerRows.Contains(z))
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
}

class RightWave : IWaveBehaviour
{
    public WaveDirection GetDirection()
    {
        return WaveDirection.RIGHT;
    }

    public void DoWave(Board board, List<int> dangerRows)
    {
        // iterate through all positions on the board, and set its value to the value to the left
        // note: iterating left to right
        for (int x = 0; x < board._pieceMapping.numXSquares; x++)
        {
            for (int z = 0; z < board._pieceMapping.numZSquares; z++)
            {
                if (dangerRows.Contains(z))
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
}


class UpWave : IWaveBehaviour
{
    public WaveDirection GetDirection()
    {
        return WaveDirection.UP;
    }

    public void DoWave(Board board, List<int> dangerRows)
    {
        // iterate through all positions on the board, and set its value to the value to the left
        // note: iterating top to bottom
        for (int x = 0; x < board._pieceMapping.numXSquares; x++)
        {
            for (int z = 0; z < board._pieceMapping.numZSquares; z++)
            {
                if (x % 2 == 1)
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
                            int[] position = new int[2] { x, z - 1 };
                            Vector3 newPosition = board._pieceMapping.GetCoordinate(position);
                            currentPiece.StartPlace(newPosition, false, true);
                        }
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

    public void DoWave(Board board, List<int> dangerRows)
    {
        // iterate through all positions on the board, and set its value to the value to the left
        // note: iterating top to bottom
        for (int x = 0; x < board._pieceMapping.numXSquares; x++)
        {
            for (int z = board._pieceMapping.numZSquares - 1; z > -1; z--)
            {
                if (x % 2 == 1)
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
}
