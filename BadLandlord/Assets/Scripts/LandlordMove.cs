using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LandlordMove : MonoBehaviour {

      //public Animator anim;
      public Rigidbody2D rb2D;
      private bool FaceRight = true; // determine which way player is facing.
      public float runSpeed = 2f;
      public bool isAlive = true;
      public bool movementEnabled = true;
      public Animator anim;
      public bool inElevator = false;

      void Start(){
           rb2D = transform.GetComponent<Rigidbody2D>();
           anim = gameObject.GetComponentInChildren<Animator>();
      }

      void Update(){
          if (inElevator == false){
              //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
              //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
              Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
             if (isAlive == true && movementEnabled == true){

                    transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;

                    if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)){
                        anim.SetBool("Walk", true);
                    } else {anim.SetBool("Walk", false);}

                        // NOTE: if input is moving the Player right and Player faces left, turn, and vice-versa
                        // if ((hvMove.x <0 && FaceRight) || (hvMove.x >0 && !FaceRight)){
                        //        playerTurn();
                        //  }}
            }
            bool flipped = hvMove.x <0;
            this.transform.rotation = Quaternion.Euler(new Vector4(0f, flipped ? 180f : 0f, 0f));
        }    
      }

      private void playerTurn(){
            // NOTE: Switch player facing label
            FaceRight = !FaceRight;

            // NOTE: Multiply player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
      }



    //public functions (used by Object State)
    public void EnableMovement()
    {
        movementEnabled = true;
    }

    public void DisableMovement()
    {
        anim.SetBool("Walk", false);
        movementEnabled = false;
    }

    public void FixAnimEnter()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Fix", true);
    }

    public void FixAnimExit()
    {
        anim.SetBool("Fix", false);
    }
}
