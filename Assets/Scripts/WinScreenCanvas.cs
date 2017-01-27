using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinScreenCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
            SceneManager.LoadScene("MainMenu");
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene("MainMenu");
        if (Input.GetMouseButton(0))
            SceneManager.LoadScene("MainMenu");
    }
}
