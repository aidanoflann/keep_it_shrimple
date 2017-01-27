using UnityEngine;
using System.Collections;

public class BackdropBehaviour : MonoBehaviour {

    public GameManager gameManager;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (this.gameManager.turn == PieceColour.WHITE)
        {
            sr.color = new Color(212f / 255f, 0.9f, 0.9f);
        }
        else if (this.gameManager.turn == PieceColour.BLACK)
        {
            sr.color = new Color(106f / 255f, 130f / 255f, 148f / 255f);
        }
    }
}
