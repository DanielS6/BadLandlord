using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenantLogic : MonoBehaviour
{
    public GameObject happinessBar;
    public GameObject tenant;
    public GameObject money;
    public GameObject dollarSignArt;
    public GameObject levelHandler;
    public GameObject date;
    public int rentAmount = 10;

    int happiness;
    bool offeringRent;

    // Last time that rent was offered
    int lastRentMonth;

    void Start()
    {
        dollarSignArt.SetActive(false);
        offeringRent = false;
        lastRentMonth = 0;
    }

    void Update()
    {
        happiness = happinessBar.GetComponent<HappinessBar>().happinessLevel;

        /* LOSE GAME if a tenant leaves */
        if (happiness <= 0){
            Debug.Log("Tenant leaving");
            /* Destroy(happinessBar, 1);
            Destroy(tenant, 1); */
            levelHandler.GetComponent<LevelHandler>().loseLevel();
        }
        /* Check if rent should be offered */
        int currentMonth = date.GetComponent<Date>().month;
        if (currentMonth != lastRentMonth) {
            // Start offering rent, and update the last month
            lastRentMonth = currentMonth;
            offerRent();
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
