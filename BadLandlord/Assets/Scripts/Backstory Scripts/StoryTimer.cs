// from https://answers.unity.com/questions/896755/enabledisable-game-objects-after-wait-for-seconds.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTimer : MonoBehaviour {
    public GameObject objectToActivate;
    public int seconds = 15;
    public bool on = true;

    private void Start() {
        StartCoroutine(ActivationRoutine());
    }

    private IEnumerator ActivationRoutine() {
        objectToActivate.SetActive(on);
        yield return new WaitForSeconds(seconds);
        objectToActivate.SetActive(!on);
    }
}
