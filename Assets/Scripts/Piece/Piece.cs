using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PieceType
{
    ROOK = 0,
    KING = 1,
    PRAWN = 2,
    KNIGHT = 3
}

public class Piece : MonoBehaviour
{

    #region private fields
    public int[] position = new int[2];
    private Board _board;
    private bool dragging = false;
    private float distance;
    private float y;
    private GameManager _gameManager; // super bad practice lol
    private List<int[]> currentLegalPositions;
    private PieceBehaviour myBehaviour;
    #endregion

    public PieceType myType;
    public PieceColour colour;
    private List<Piece> deathList;
    public bool isFirstMove = true;

    public void Place(Vector3 position, bool triggerTurnChange = true)
    // place the piece - snapping to nearest board position.
    {
        this.transform.position = position;
        bool validMove = false;
        int[] candidatePosition = _board.GetNearestPosition(this);
        foreach (int[] test in currentLegalPositions)
        {
            if (candidatePosition[0] == test[0] && candidatePosition[1] == test[1])
            {
                validMove = true;
                break;
            }
        }
        if (validMove)
        {
            this.transform.position = _board.GetCoordinate(candidatePosition);
            this.position = candidatePosition;
            //check if there is an existing piece and destroy it if there is
            foreach (Piece candidate in deathList)
            {
                if (candidate.position[0] == this.position[0] && candidate.position[1] == this.position[1])
                    Object.Destroy(candidate.gameObject);
            }
            isFirstMove = false;

            if (triggerTurnChange)
            {
                this._gameManager.TurnChange();
            }
        }
        else
        {
            this.transform.position = _board.GetCoordinate(this.position);
        }
    }

    public void StartPlace(Vector3 position, bool triggerTurnChange = true)
    // place the piece - snapping to nearest board position.
    {
        this.transform.position = position;
        this.position = _board.GetNearestPosition(this);
        this.transform.position = _board.GetCoordinate(this.position);
        if (triggerTurnChange)
        {
            this._gameManager.TurnChange();
        }
    }

    #region MonoBehaviour utilities
    // Use this for initialization
    void Start()
    {
        this._board = FindObjectOfType<Board>();
        this._gameManager = FindObjectOfType<GameManager>();
        this.y = transform.position.y;
        this.StartPlace(this.transform.position, false);
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        Color shaderColor;
        if (this.colour == PieceColour.BLACK)
        {
            shaderColor = Color.black;
        }
        else
        {
            shaderColor = Color.white;
        }
        shaderColor.a = 0.9f;
        rend.material.SetColor("_Color", shaderColor);

        switch (myType)
        {
            case PieceType.KING:
                myBehaviour = new KingBehaviour();
                break;
            case PieceType.ROOK:
                myBehaviour = new RookBehaviour();
                break;
            case PieceType.PRAWN:
                myBehaviour = new PrawnBehaviour();
                break;
            case PieceType.KNIGHT:
                myBehaviour = new KnightBehaviour();
                break;
            default:
                myBehaviour = new KingBehaviour();
                break;
        }
    }

    void OnMouseDown()
    {
        if (this.colour == this._gameManager.turn)
        {
            currentLegalPositions = myBehaviour.legalPositions(_board, this, out this.deathList);
            if (currentLegalPositions.Count != 0)
            {
                distance = Vector3.Distance(transform.position, Camera.main.transform.position);
                dragging = true;
            }
        }
    }

    void OnMouseUp()
    {
        if (dragging == true)
        {
            dragging = false;
            this.Place(transform.position); //position it will be placed at
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            rayPoint.y = this.y;  //The Stevie Hack
            transform.position = rayPoint;
        }
    }

    public int[] startPosition()
    {
        return position;
    }

    #endregion
}
