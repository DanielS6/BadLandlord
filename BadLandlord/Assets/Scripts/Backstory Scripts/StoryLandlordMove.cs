using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLandlordMove : MonoBehaviour
{
    public Animator anim;
    public float speed = 2f;
    public Rigidbody2D rb;
    public int maxWalkCount = 100;
    bool isFacingRight = true;
    int counter = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, rb.velocity.y);
        anim.SetBool("Walk", true);
    }
    void FixedUpdate()
    {
        if (counter == maxWalkCount){ //TODO will be replaced with collision
            rb.velocity = new Vector2(0, 0);
            counter = 0;
            anim.SetBool("Walk", false);
        } else {
            counter++;
        }
        this.transform.rotation = Quaternion.Euler(new Vector4(0f, isFacingRight ? 0f : 180f, 0f));
    }
}
