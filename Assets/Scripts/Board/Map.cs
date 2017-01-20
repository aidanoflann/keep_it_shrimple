using UnityEngine;
using System.Collections;

public class Map
{
    // class for storing mappings of actual coordinates to chess-style positions

    private int _numXSquares = 8;
    private int _numYSquares = 8;
    private Vector3[,] _coordinates;

    public Map (Vector3 min, Vector3 size)
    {
        this.InitCoordinates(min, size);
    }

    public void InitCoordinates(Vector3 min, Vector3 size)
    {
        this._coordinates = new Vector3[this._numXSquares, this._numYSquares];

        for (int x = 0; x < this._numXSquares; x++)
        {
            for (int y = 0; y < this._numYSquares; y++)
            {
                // TODO calculate _coordinates [x, y]
            }
        }
    }

}
