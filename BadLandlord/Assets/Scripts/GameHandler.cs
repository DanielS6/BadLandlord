using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    void Update() {
        if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }
}
