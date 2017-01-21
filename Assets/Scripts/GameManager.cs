using UnityEngine;
using System.Collections;

public enum PieceColour
{
    BLACK = 0,
    WHITE = 1
}

public class GameManager : MonoBehaviour
{
    public PieceColour turn;
    private WaveManager _waveManager;
    private Board _board;
    private Animator _cameraAnimator;

    public bool wavesEnabled;
    public bool cameraRotateEnabled;

    // Use this for initialization
    void Start()
    {
        this.turn = PieceColour.BLACK;
        this._waveManager = new WaveManager();
        this._board = FindObjectOfType<Board>();
        this._cameraAnimator = GameObject.Find("MainCamera").GetComponent<Animator>();
        this.TurnChange(false);
    }

    // Update is called once per frame
    void Update()
    {
        this._waveManager.Tick(this._board);
    }

    public void TurnChange(bool triggeredByPlayer=true)
    {
        if (this.turn == PieceColour.BLACK)
        {
            this.turn = PieceColour.WHITE;
            Camera.main.backgroundColor = Color.white;
        }
        else if (this.turn == PieceColour.WHITE)
        {
            this.turn = PieceColour.BLACK;
            Camera.main.backgroundColor = Color.black;
        }
        // wavemanager will check if the board needs to be waved, and if so, waves it.
        if (this.wavesEnabled && this._waveManager.IsAWaveDue)
        {
            this._waveManager.TriggerWave();
        }
        if (cameraRotateEnabled && triggeredByPlayer)
        {
            _cameraAnimator.SetTrigger("RotateCamera");
        }
    }
}