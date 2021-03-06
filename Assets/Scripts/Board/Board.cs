﻿using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    #region private fields
    //List of the positions a piece can be placed on
    public Map _pieceMapping;
    //vector of positions on the map. If element != null piece is in this location
    public Piece[,] _pieces;
    #endregion

    public int[] GetNearestPosition( Piece piece, bool deleteOldPositions=true)
    // Given a position in game coordinates, calculate the nearest coordinates on the Board.
    {
        Vector3 coordinates = piece.transform.position;
        int[] newPosition = this._pieceMapping.GetNearestPosition(coordinates);
        return newPosition;
    }

    public Vector3 GetCoordinate(int[] position)
    {
        return this._pieceMapping.GetCoordinate(position);
    }

    public void MovePiece( Piece piece, int[] newPosition, bool deleteOldPosition=true)
    {
        if (deleteOldPosition)
        {
            this._pieces[piece.position[0], piece.position[1]] = null;
        }
        this._pieces[newPosition[0], newPosition[1]] = piece;
    }

    void Awake()
    {
        MeshFilter mf = this.gameObject.GetComponent<MeshFilter>();
        Vector3 min = mf.mesh.bounds.min;
        Vector3 size = mf.mesh.bounds.size;
        float yRotation = this.gameObject.transform.rotation.y;
        this._pieceMapping = new Map(min, size, yRotation);
        this._pieces = new Piece[this._pieceMapping.numXSquares, this._pieceMapping.numZSquares];
    }

    // Use this for initialization
    void Start() {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

}
