using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandlordElevator : MonoBehaviour
{
    private GameObject currElevator;
    public float elevatorSpeed=1f;
	bool goingUp = false;
	bool goingDown = false;
	Transform newFloor;
    public SpriteRenderer playerSprite;

    // Update is called once per frame
    void Update() {
        // Can't do anything if the current elevator isn't set
        if (currElevator == null) {
            return;
        }
        Elevator elevatorComponent = currElevator.GetComponent<Elevator>();
        // W - up for now
        if (Input.GetKeyDown(KeyCode.W)) {
            // Don't break if there is no up destination, e.g. for the top floor
            if ( elevatorComponent.GetDestinationUp() ) {
                //transform.position = elevatorComponent.GetDestinationUp().position;
				newFloor = elevatorComponent.GetDestinationUp();
				goingUp=true;
                //playerSprite.sortingOrder = 0;
                
            }
        }

        // S - down for now
        if (Input.GetKeyDown(KeyCode.S)) {
            // Don't break if there is no down destination, e.g. for the bottom floor
            if ( elevatorComponent.GetDestinationDown() ) {
                //transform.position = elevatorComponent.GetDestinationDown().position;
				newFloor = elevatorComponent.GetDestinationDown();
                goingDown=true;
                
            }
        }
    }

	void FixedUpdate(){
		if(goingUp){
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<LandlordMove>().enabled = false;
            playerSprite.sortingOrder = -10;
            
			Vector2 pos = Vector2.Lerp ((Vector2)transform.position, (Vector2)newFloor.position+ new Vector2(0,0.5f), elevatorSpeed * Time.fixedDeltaTime);
			transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
			if (transform.position.y >= newFloor.position.y){
				GetComponent<Collider2D>().enabled = true;
                GetComponent<Rigidbody2D>().isKinematic = false;
                GetComponent<LandlordMove>().enabled = true;
                playerSprite.sortingOrder = 100;
                goingUp=false;
			}
		}

		if(goingDown){
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<LandlordMove>().enabled = false;
            playerSprite.sortingOrder = -10;
            
			Vector2 pos = Vector2.Lerp ((Vector2)transform.position, (Vector2)newFloor.position- new Vector2(0,0.5f), elevatorSpeed * Time.fixedDeltaTime);
			transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
			if (transform.position.y <= newFloor.position.y){
				GetComponent<Collider2D>().enabled = true;
                GetComponent<Rigidbody2D>().isKinematic = false;
                GetComponent<LandlordMove>().enabled = true;
                playerSprite.sortingOrder = 100;
                goingDown=false;
			}
		}

	}


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Landlord interaction, give options for fixing
        if (other.CompareTag("Elevator"))
        {
            currElevator = other.gameObject;
            //elevatorDoors = other.gameObject.GetComponentInChildren<SpriteRenderer>();
            playerSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
            //playerSprite.sortingOrder = 0;
            
            //anim.SetBool("get_on", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // hide interact prompt
        if (other.CompareTag("Elevator"))
        {
            if (other.gameObject == currElevator)
            {
                currElevator = null;
            }
            //anim.SetBool("get_on", false);
        }
    }
}
