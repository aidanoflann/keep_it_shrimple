using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    #region private fields
    private Map _pieceMapping;
    #endregion

    public Vector3 GetNearestPosition( Vector2 coordinates)
    // Given a position in game coordinates, calculate the nearest chessboard position (e.g. [3, 1]).
    {
        return this._pieceMapping.GetNearestPosition(coordinates);
    }

    // Use this for initialization
    void Start() {
        MeshFilter mf = this.gameObject.GetComponent<MeshFilter>();
        Vector3 min = mf.mesh.bounds.min;
        Vector3 size = mf.mesh.bounds.size;
        float yRotation = this.gameObject.transform.rotation.y;
        this._pieceMapping = new Map(min, size, yRotation);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
