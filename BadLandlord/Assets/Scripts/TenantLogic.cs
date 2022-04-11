using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenantLogic : MonoBehaviour
{
    public GameObject happinessBar;
    public GameObject tenant;
    public GameObject money;
    public GameObject dollarSignArt;
    public int rentAmount = 10;

    int happiness;
    bool offeringRent;

    void Start()
    {
        dollarSignArt.SetActive(false);
        offeringRent = false;
        //happinessBar = happinessBar.GetComponent<HappinessBar>();
    }

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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && offeringRent){
            giveRent();
            
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
        money.GetComponent<MoneyBar>().addMoney(rentAmount);
        offeringRent = false;
    }
}
