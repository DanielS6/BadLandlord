using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    // This script is added to the PauseHandler object, various other
    // components call the methods
    public GameObject pauseButton;
    public GameObject pauseMenu;

    void Start() {
        // Pause menu starts hidden
        pauseMenu.SetActive(false);
    }

    public void PauseGame() {
        Debug.Log("Should pause");
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame() {
        Debug.Log("Should resume");
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void QuitGame() {
        Debug.Log("Should quit");
        // Ensure time is running again for anything else in the project,
        // doesn't matter for the current scene since we are leaving
        Time.timeScale = 1f;
        SceneManager.LoadScene("IntroScene");
    }
}
