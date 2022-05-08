using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationBar : MonoBehaviour {
    public GameObject notificationText;

    // Knowledge of which apartments have unhappy tenants
    private Dictionary<int, bool> apartmentHappiness = new Dictionary<int, bool>();

    // Whether bills are waiting to be paid
    private bool pendingBills = false;

    void Start() {
        setDisplayText("");
    }

    void Update() {
        // Figure out how many tenants are unhappy, and which ones those are,
        // to display a message
        List<int> unhappyTenants = new List<int>();
        foreach ( int apartmentNum in apartmentHappiness.Keys ) {
            if ( apartmentHappiness[apartmentNum] == false ) {
                unhappyTenants.Add( apartmentNum );
            }
        }
        // Base case: 0 unhappy tenants, only need to handle bills
        int numUnhappyTenants = unhappyTenants.Count;
        if ( numUnhappyTenants == 0 ) {
            // Check for bills
            if ( pendingBills ) {
                setDisplayText( "The bills need to be paid!" );
            } else {
                setDisplayText( "" );
            }
            return;
        }
        // Deduplicate logic for saying the bills need to be paid in addition
        // to one or more unhappy tenants.
        string messageEnd = "!";
        if ( pendingBills ) {
            messageEnd = ", and the bills need to be paid!";
        }
        // Easy case: 1 unhappy tenant
        if ( numUnhappyTenants == 1 ) {
            setDisplayText(
                "The tenant in apartment " + unhappyTenants[0]
                    + " is unhappy and about to leave" + messageEnd
            );
            return;
        }
        // Construct longer message for multiple apartments
        string leaveMessage = "The tenants in apartments "
            + unhappyTenants[ 0 ];
        for ( int iii = 1; iii < numUnhappyTenants - 1; iii++ ) {
            leaveMessage += ", " + unhappyTenants[ iii ];
        }
        leaveMessage += " and " + unhappyTenants[ numUnhappyTenants - 1 ]
            + " are unhappy and about to leave" + messageEnd;
        setDisplayText( leaveMessage );
    }

    // Update the apartmentHappiness dictionary for a specific tenant being
    // happy or not
    public void SetTenantHappiness(int apartmentNum, bool isHappy) {
        apartmentHappiness[apartmentNum] = isHappy;
    }

    public void SetBillsPending(bool arePending) {
        pendingBills = arePending;
    }

    private void setDisplayText(string textToDisplay){
        Text curMessage = notificationText.GetComponent<Text>();
        curMessage.text = textToDisplay;
    }
}
