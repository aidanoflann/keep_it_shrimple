using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour {

    void Start() {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void Update() {
        if (Input.GetMouseButtonUp(0))
            SceneManager.LoadScene("MainMenu");
    }
}
