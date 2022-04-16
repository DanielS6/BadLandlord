using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

    // This script is used by both the pause menu and the pause button
    // components to avoid splitting the logic in multiple files
    public GameObject pauseButton;
    public GameObject pauseMenu;

    void Start() {
        // Pause menu starts hidden
        pauseMenu.SetActive(false);
    }

    public void PauseGame() {
        Debug.Log("Should pause");
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame() {
        Debug.Log("Should resume");
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }
}
