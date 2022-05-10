using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    [SerializeField] private Transform destinationUp;
    [SerializeField] private Transform destinationDown;
    private SpriteRenderer controlUp;
    private SpriteRenderer controlDown;
    //private GameObject currElevator;
    //private bool top;
    //private bool bottom;
    //private int currFloor;
    //public Transform[] elevators;
    //private int range;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //range = 1;
        //top = false;
        //bottom = true;
        //currFloor = 0;
        //currElevator = elevators[currFloor-1];
        anim = gameObject.GetComponentInChildren<Animator>();
        controlUp = transform.Find("WUp").GetComponent<SpriteRenderer>();
        controlUp.enabled = false;
        controlDown = transform.Find("SDown").GetComponent<SpriteRenderer>();
        controlDown.enabled = false;
    }
    
    public Transform GetDestinationUp()
    {
        return destinationUp;
    }
    
    public Transform GetDestinationDown()
    {
        return destinationDown;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Landlord interaction, give options for fixing
        if (other.CompareTag("Player"))
        {
            controlUp.enabled = true;
            controlDown.enabled = true;
            anim.SetBool("get_on", true);
        } 
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        // hide interact prompt
        if (other.CompareTag("Player"))
        {
            controlUp.enabled = false;
            controlDown.enabled = false;
            anim.SetBool("get_on", false);
        }
    }
}
