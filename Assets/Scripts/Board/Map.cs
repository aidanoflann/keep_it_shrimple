using UnityEngine;
using System.Collections;

public class Map
{
    // class for storing mappings of actual coordinates to chess-style positions

    private int _numXSquares = 8;
    private int _numZSquares = 8;
    private Vector3[,] _coordinates;

    // constructor
    public Map (Vector3 min, Vector3 size, float yRotation)
    {
        this._coordinates = new Vector3[this._numXSquares, this._numZSquares];
        this.CalculateCoordinates(min, size, yRotation);
    }

    public int[] GetNearestPosition(Vector3 point)
    {
        float closestDistance = 10000f;
        int[] position = new int[2];
        for (int x = 0; x < this._numXSquares; x++)
        {
            for (int z = 0; z < this._numZSquares; z++)
            {
                if (Vector3.Distance(point, this._coordinates[x, z]) < closestDistance)
                {
                    position[0] = x;
                    position[1] = z;
                }
            }
        }
        return position;
    }

    public void CalculateCoordinates(Vector3 min, Vector3 size, float yRotation)
    // min: extreme corner of mesh
    // size: full size of mesh (only care about x and z)
    // yRotation: angle in degrees about which map has been rotated.
    { 
        for (int x = 0; x < this._numXSquares; x++)
        {
            for (int z = 0; z < this._numZSquares; z++)
            {
                _coordinates[x, z].x = min.x + ( ((float)x + 0.5f) / this._numXSquares) * size.x;
                _coordinates[x, z].z = min.z + (((float)z + 0.5f) / this._numZSquares) * size.z;
            }
        }
    }
}
