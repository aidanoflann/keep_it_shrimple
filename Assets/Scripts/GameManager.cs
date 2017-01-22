using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PieceColour
{
    NONE = -1,
    BLACK = 0,
    WHITE = 1
}

public class GameManager : MonoBehaviour
{
    public PieceColour turn;
    private WaveManager _waveManager;
    private Board _board;
    private Animator _cameraAnimator;
    public PieceColour theWinner =  PieceColour.NONE;
    private Canvas winScreenCanvas;
    private bool isWinScreen = false;

    public bool wavesEnabled;
    public bool cameraRotateEnabled;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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
        if (this.isWinScreen)
        {
            if (this.theWinner == PieceColour.BLACK)
            {
                GameObject.Find("WhitePlayerWins").GetComponent<Text>().enabled = false;
            }
            else if (this.theWinner == PieceColour.WHITE)
            {
                GameObject.Find("BlackPlayerWins").GetComponent<Text>().enabled = false;
            }
        }
        this._waveManager.Tick(this._board);
        if (this.theWinner != PieceColour.NONE)
        {
            this.TriggerEndGame();
        }
    }

    public void TriggerEndGame()
    {
        SceneManager.LoadScene("Winscreen");
        this.isWinScreen = true;
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