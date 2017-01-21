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

    // Use this for initialization
    void Start()
    {
        this.turn = PieceColour.BLACK;
        this._waveManager = new WaveManager();
        this._board = FindObjectOfType<Board>();
        this.TurnChange();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnChange()
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
        this._waveManager.ApplyWaveToBoard(this._board);
    }
}