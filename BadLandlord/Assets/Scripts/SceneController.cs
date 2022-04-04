using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    // Start the game, from the button on the start canvas
    public void LaunchGameScene() {
        SceneManager.LoadScene( "MainScene" );
    }

}
