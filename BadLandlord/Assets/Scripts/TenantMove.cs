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
        this.transform.rotation = Quaternion.Euler(new Vector4(0f, isFacingRight ? 0f : 180f, 0f));
        // Also need to flip the Z value so that it doesn't disappear
        // when facing left. Z should be positive when facing left, negative
        // when facing right
        float currentZ = this.transform.position.z;
        float positiveZ = ( currentZ < 0.0f ? ( -1.0f * currentZ ) : currentZ );
        Vector3 correctPos = new Vector3(
            this.transform.position.x,
            this.transform.position.y,
            ( isFacingRight ? ( -1.0f * positiveZ ) : positiveZ )
        );
        this.transform.position = correctPos;
    }
}
