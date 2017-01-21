using UnityEngine;
using System.Collections;

public enum PieceColour
{
    BLACK = 0,
    WHITE = 1
}

public class GameManager : MonoBehaviour
{
    public PieceColour turn;

    // Use this for initialization
    void Start()
    {
        this.turn = PieceColour.BLACK;
        this.TurnChange();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnChange()
    {
        if (this.turn == PieceColour.BLACK)
        {
            this.turn = PieceColour.WHITE;
            Camera.main.backgroundColor = Color.white;
        }
        else if (this.turn == PieceColour.WHITE)
        {
            this.turn = PieceColour.BLACK;
            Camera.main.backgroundColor = Color.black;
        }
    }
}