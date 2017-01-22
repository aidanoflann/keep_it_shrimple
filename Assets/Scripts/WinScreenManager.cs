using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour {

    void Update() {
        if (Input.GetMouseButtonUp(0))
            SceneManager.LoadScene("MainMenu");
    }
}
