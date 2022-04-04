using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandlordInteract : MonoBehaviour
{
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        // grab the rigidbody from the player object
        rb = GetComponent<Rigidbody2D> ();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
