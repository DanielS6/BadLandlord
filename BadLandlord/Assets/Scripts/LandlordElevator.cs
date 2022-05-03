using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandlordElevator : MonoBehaviour
{
    private GameObject currElevator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //up for now
        {
            if (currElevator != null)
            {
                transform.position = currElevator.GetComponent<Elevator>().GetDestinationUp().position;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.W)) // down for now
        {
            if (currElevator != null)
            {
                transform.position = currElevator.GetComponent<Elevator>().GetDestinationDown().position;
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
