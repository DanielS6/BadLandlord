using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* overarching handler for all objects
 * every specified interval, gathers all objects and chooses randomly
 * if object is safe, nothing happens
 * otherwise, uses corresponding probability to test if it breaks
*/
public class ObjectHandler : MonoBehaviour
{
    public float TRIALFREQ; // freq of possibly breaking object
    private float gameTimer;
    private System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        gameTimer = 0f;

    }

    private void FixedUpdate()
    {
        // increase timer and call break trial upon TRIALFREQ interval
        gameTimer += 0.01f;
        if (gameTimer > TRIALFREQ)
        {
            ObjectBreakTrial();
            gameTimer = 0;
        }
    }

    //randomly picks one object to call a break trial for
    private void ObjectBreakTrial()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("ObjectTag");
        int index = rnd.Next(allObjects.Length);
        GameObject chosen = allObjects[index];
        chosen.GetComponent<ObjectState>().ObjectBreakTrial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
