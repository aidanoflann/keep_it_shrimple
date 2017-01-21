using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

    #region private fields
    private int[] _position = new int[2];
    private Board _board;
    #endregion

    public void Place(Vector3 position)
    // place the piece - snapping to nearest board position.
    {
        transform.position = _board.GetNearestPosition(transform.position);
    }

    #region MonoBehaviour utilities
    // Use this for initialization
    void Start () {
        this._board = FindObjectOfType<Board>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    #endregion
}
