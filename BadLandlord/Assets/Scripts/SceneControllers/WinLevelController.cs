using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevelController : MonoBehaviour {

    // Advance to the "next" level. For now, we only have one level, so
    // instead this just takes the user to the overall victory scene
    public void PlayNextLevel() {
        // TODO once we have multiple levels, make it possible to configure
        // what the "next" scene is to avoid needing to duplicate this
        SceneManager.LoadScene( "WinGame" );
    }
}
