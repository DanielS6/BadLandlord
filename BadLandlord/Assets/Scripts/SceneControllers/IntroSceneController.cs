using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneController : MonoBehaviour {

    // Start the game, goes to the instructions scene and from there to Level1
    public void LaunchInstructionsScene() {
        SceneManager.LoadScene( "InstructionsScene" );
    }

}
