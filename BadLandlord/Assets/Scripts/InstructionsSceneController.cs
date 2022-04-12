using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsSceneController : MonoBehaviour {

    // Go back to intro scene
    public void LaunchIntroScene() {
        SceneManager.LoadScene( "IntroScene" );
    }

}
