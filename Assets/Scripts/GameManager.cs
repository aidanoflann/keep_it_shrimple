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
        this.turn = PieceColour.WHITE;
    }

    // Update is called once per frame
    void Update()
    {

    }
}