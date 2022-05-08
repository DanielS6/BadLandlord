using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    // This script is added to the PauseHandler object, various other
    // components call the methods
    public GameObject pauseButton;
    public GameObject pauseMenu;

    // Controlling the volume of the music
    public AudioMixer mixer;

    // State to know what to do when the escape key is pressed, either pause
    // or resume
    private bool currentlyPaused;

    void Start() {
        // Pause menu starts hidden
        currentlyPaused = false;
        pauseMenu.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown("escape")) {
            // Use GetKeyDown() instead of GetKey() so that when the key is
            // held for multiple frames (which is almost guaranteed to happen)
            // we only toggle the state once
            if (currentlyPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void SetVolumeLevel(float sliderVolume) {
        mixer.SetFloat( "MusicVolume", Mathf.Log10(sliderVolume) * 20 );
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        currentlyPaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        currentlyPaused = false;
    }

    public void RestartLevel() {
        // Named restart level and made scene-agnostic in case we later switch
        // to having multiple game scenes
        // Time should run again for the resumed scene, and no longer paused
        Time.timeScale = 1f;
        currentlyPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame() {
        // Ensure time is running again for anything else in the project,
        // doesn't matter for the current scene since we are leaving, same
        // with pause state
        Time.timeScale = 1f;
        currentlyPaused = false;
        SceneManager.LoadScene("IntroScene");
    }
}
