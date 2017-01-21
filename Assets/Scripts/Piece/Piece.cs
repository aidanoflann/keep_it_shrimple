﻿using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

    #region private fields
    public int[] position = new int[2];
    private Board _board;
    private bool dragging = false;
    private float distance;
    private float y;
    private GameManager _gameManager; // super bad practice lol
    #endregion

    public PieceColour colour;

    public void Place(Vector3 position, bool triggerTurnChange=true)
    // place the piece - snapping to nearest board position.
    {
        this.position = _board.GetNearestPosition(this);
        this.transform.position = _board.GetCoordinate(this.position);
        if (triggerTurnChange)
        {
            this._gameManager.TurnChange();
        }
    }

    #region MonoBehaviour utilities
    // Use this for initialization
    void Start () {
        this._board = FindObjectOfType<Board>();
        this._gameManager = FindObjectOfType<GameManager>();
        this.y = transform.position.y;
        this.Place(this.transform.position, false);
	}

    void OnMouseDown()
    {
        if (this.colour == this._gameManager.turn)
        {
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
        }
    }

    void OnMouseUp()
    {
        if (dragging == true)
        {
            dragging = false;
            this.Place(transform.position);
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
    #endregion
}
