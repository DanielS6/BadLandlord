using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGameController : MonoBehaviour {

    // Return to the intro scene to play the game again
    public void PlayAgain() {
        SceneManager.LoadScene( "IntroScene" );
    }

}
