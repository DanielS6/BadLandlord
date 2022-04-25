using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject player;
    public Animator anim; 
    private bool top;
    private bool bottom;
    private int currFloor;
    public Transform[] elevators;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //top = false;
        //bottom = true;
        //currFloor = 0;
        anim = anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Landlord interaction, give options for fixing
        if (other.CompareTag("Player"))
        {
            // if (currFloor == 0)
            // {
            //     bottom = true;
            //     top = false;
            // }
            // if (currFloor == 1)
            // {
            //     bottom = false;
            //     top = true;
            // }
            
            if (Input.GetMouseButtonDown(0))
            {
                player.transform.position = new Vector3(4.91f, 0.45f, 0f);
            }
            anim.SetBool("get_on", true);
        } 
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        // hide interact prompt
        if (other.CompareTag("Player"))
        {
            anim.SetBool("get_on", false);
        }
    }
}
