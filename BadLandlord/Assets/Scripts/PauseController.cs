using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour {

    // This script is added to the PauseHandler object, various other
    // components call the methods
    public GameObject pauseButton;
    public GameObject pauseMenu;

    // Restart button is not shown on non-level scenes
    public GameObject restartButton;

    // Controlling the volume of the music
    public AudioMixer mixer;

    // Persist both the volume and the slider location across scenes by
    // having a reference to the slider and a static variable with the slider
    // value, so that on start we can restore the location
    public Slider volumeSlider;
    private static float rawVolumeSliderVal = -1.0f;

    // State to know what to do when the escape key is pressed, either pause
    // or resume
    private bool currentlyPaused;

    void Start() {
        // Pause menu starts hidden
        currentlyPaused = false;
        pauseMenu.SetActive(false);

        // Wtihin the pause menu, the "restart" button is only available
        // on scenes for a game level
        restartButton.SetActive(IsLevelScene());

        // Make use of the stored volume if it was set in a prior scene
        if ( rawVolumeSliderVal != -1.0f ) {
            mixer.SetFloat(
                "MusicVolume",
                Mathf.Log10(rawVolumeSliderVal) * 20
            );
            volumeSlider.value = rawVolumeSliderVal;
        }
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

    private bool IsLevelScene() {
        // Check if we are currently in one of the level scenes
        Scene currentScene = SceneManager.GetActiveScene();
        string currentSceneName = currentScene.name;
        switch ( currentSceneName ) {
            case "Level1":
            case "Level2":
            case "level3":
            case "levelfour":
            case "level5":
                return true;
            default:
                return false;
        }
    }

    public void SetVolumeLevel(float sliderVolume) {
        mixer.SetFloat( "MusicVolume", Mathf.Log10(sliderVolume) * 20 );
        // Store the value so that we can persist it across scenes
        rawVolumeSliderVal = sliderVolume;
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
        // Restart the current scene, whatever that is
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
