using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneController : MonoBehaviour {

    // Start the game, from the button on the start canvas
    public void LaunchGameScene() {
        SceneManager.LoadScene( "Level1" );
    }

    // Switch to instructions
    public void LaunchInstructionsScene() {
        SceneManager.LoadScene( "InstructionsScene" );
    }

}
