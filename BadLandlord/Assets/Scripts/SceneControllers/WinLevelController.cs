using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevelController : MonoBehaviour {

    // Static entry point to be called by code in the actual game scenes,
    // launch the WinLevel scene with knowledge of which scene was just
    // completed, to know where to go next.
    // For now, known levels and progressions:
    // MainScene -> Level2 -> WinGame
    private static string nextLevelScene = "";
    public static void LaunchWinLevelScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        string currentSceneName = currentScene.name;
        if ( currentSceneName == "MainScene" ) {
            nextLevelScene = "Level2";
        } else {
            // Should only be called from within level 2 in that case
            nextLevelScene = "WinGame";
        }
        SceneManager.LoadScene("WinLevel");
    }

    // Advance to the "next" level, based on the already processed name,
    public void PlayNextLevel() {
        if (nextLevelScene == "") {
            Debug.Log("Unknown scene to advance to! Skipping to end.");
            nextLevelScene = "WinGame";
        }
        string sceneToLoad = nextLevelScene;
        // clear the data
        nextLevelScene = "";
        SceneManager.LoadScene( sceneToLoad );
    }
}
