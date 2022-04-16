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
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame() {
        Debug.Log("Should resume");
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void QuitGame() {
        Debug.Log("Should quit");
        SceneManager.LoadScene("IntroScene");
    }
}
