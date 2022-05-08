using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevelController : MonoBehaviour {

    // Helper to get the next level name, using early returns instead of
    // break statements
    private static string getNextLevelName(string currentLevelName) {
        switch( currentLevelName ) {
            case "Level1":
                // Level1 -> Level2
                return "Level2";
            case "Level2":
                // Level2 -> Level3
                return "level3";
            case "level3":
                // Level3 -> Level4
                return "levelfour";
            case "levelfour":
                // Level4 -> Level5
                return "level5";
            case "level5":
                return "WinGame";
            default:
                // Unknown
                return "";
        }
    }

    // Static entry point to be called by code in the actual game scenes,
    // launch the WinLevel scene with knowledge of which scene is next, based
    // on the current scene
    private static string nextLevelScene = "";
    public static void LaunchWinLevelScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        string currentSceneName = currentScene.name;
        nextLevelScene = WinLevelController.getNextLevelName( currentSceneName );
        SceneManager.LoadScene("WinLevel");
    }

    // Advance to the "next" level, based on the already processed name
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
