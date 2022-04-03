using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenantMove : MonoBehaviour
{
    public float speed = 2f;
    public Rigidbody2D rb;
    bool isFacingRight = true;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
    void FixedUpdate()
    {
        if (counter == 200){ //TODO will be replaced with collision
            if (isFacingRight){
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            } else {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            isFacingRight = !isFacingRight;
            counter = 0; 
        } else {
            
            counter++;
        }
        
    }
}
