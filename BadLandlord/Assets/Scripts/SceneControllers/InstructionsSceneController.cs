using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsSceneController : MonoBehaviour {

    // Actually start the first level
    public void LaunchGameScene() {
        SceneManager.LoadScene( "Level1" );
    }

}
