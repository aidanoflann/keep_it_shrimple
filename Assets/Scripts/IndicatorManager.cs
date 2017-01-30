using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IndicatorManager {

    private GameObject _original;
    private List<GameObject> _copies = new List<GameObject>();

    public IndicatorManager()
    {
        this._original = GameObject.Find("highlight");
    }

    public void IndicateAllPositions(Board board, List<int[]> positions)
    {
        foreach(int[] position in positions)
        {
            Vector3 coordinates = board.GetCoordinate(position);
            this.Indicate(coordinates);
        }
    }

    public void Indicate(Vector3 coordinates)
    // generate a copy of the original highlighter and move it to the given coordinates
    {
        GameObject clone = GameObject.Instantiate(this._original);
        clone.transform.position = coordinates;
        this._copies.Add(clone);
    }

    public void Clear()
    // iterate over each stored GameObject and delete each of them.
    {
        foreach(GameObject copy in this._copies)
        {
            GameObject.Destroy(copy);
        }
        this._copies.Clear();
    }
}