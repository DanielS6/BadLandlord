using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsSceneController : MonoBehaviour {

    // Display the backstory before starting the game
    public void LaunchBackstoryScene() {
        SceneManager.LoadScene( "BackstoryScene" );
    }

}
