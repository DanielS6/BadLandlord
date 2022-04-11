using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenantLogic : MonoBehaviour
{
    public GameObject happinessBar;
    public GameObject tenant;
    public GameObject dollarSignArt;

    int happiness;
    bool offeringRent;

    void Start()
    {
        dollarSignArt.SetActive(false);
        offeringRent = false;
        //happinessBar = happinessBar.GetComponent<HappinessBar>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO update so not constantly getting component
        happiness = happinessBar.GetComponent<HappinessBar>().happinessLevel;
        //happiness = happinessBar.happinessLevel;
        
        if (happiness <= 0){
            Debug.Log("Tenant leaving");
            Destroy(happinessBar, 1);
            Destroy(tenant, 1);
            
        }
        
    }
    public void offerRent()
    {
        dollarSignArt.SetActive(true);
        offeringRent = true;
    }
    public void giveRent()
    {
        dollarSignArt.SetActive(false);
        offeringRent = false;
    }
}
