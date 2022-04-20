using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseLevelController : MonoBehaviour {
    // Static entry point to be called by code in the actual game scenes,
    // launch the LoseLeveL scene with a current scene having been lost, so
    // that we know which one to restart
    private static string sceneToRestart = "";
    public static void LaunchLoseLevelScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        string currentSceneName = currentScene.name;
        sceneToRestart = currentSceneName;
        SceneManager.LoadScene("LoseLevel");
    }

    public void TryLevelAgain() {
        if (sceneToRestart == "") {
            Debug.Log("Unknown scene to restart! Quiting instead");
            QuitGame();
            return;
        }
        // Clear the data
        string nextScene = sceneToRestart;
        sceneToRestart = "";
        SceneManager.LoadScene(nextScene);
    }

    public void QuitGame() {
        // Clear the data
        sceneToRestart = "";
        SceneManager.LoadScene("IntroScene");
    }
}
