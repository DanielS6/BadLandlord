using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandlordElevator : MonoBehaviour
{
    private GameObject currElevator;

    // Update is called once per frame
    void Update() {
        // Can't do anything if the current elevator isn't set
        if (currElevator == null) {
            return;
        }
        Elevator elevatorComponent = currElevator.GetComponent<Elevator>();
        // E - up for now
        if (Input.GetKeyDown(KeyCode.E)) {
            // Don't break if there is no up destination, e.g. for the top floor
            if ( elevatorComponent.GetDestinationUp() ) {
                transform.position = elevatorComponent.GetDestinationUp().position;
            }
        }

        // W - down for now
        if (Input.GetKeyDown(KeyCode.W)) {
            // Don't break if there is no down destination, e.g. for the bottom floor
            if ( elevatorComponent.GetDestinationDown() ) {
                transform.position = elevatorComponent.GetDestinationDown().position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Landlord interaction, give options for fixing
        if (other.CompareTag("Elevator"))
        {
            currElevator = other.gameObject;
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
