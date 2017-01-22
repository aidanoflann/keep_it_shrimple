using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour {

    void OnMouseDown()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
