using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

    #region private fields
    private int[] _position = new int[2];
    private Board _board;
    private bool dragging = false;
    private float distance;
    #endregion

    public void Place(Vector3 position)
    // place the piece - snapping to nearest board position.
    {
        this._position = _board.GetNearestPosition(this);
        this.transform.position = _board.GetCoordinate(this._position);
    }

    #region MonoBehaviour utilities
    // Use this for initialization
    void Start () {
        this._board = FindObjectOfType<Board>();
	}

    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
        this.Place(transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            rayPoint.y = 0.15f;  //The Stevie Hack
            transform.position = rayPoint;
        }
    }
    #endregion
}
