using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenantLogic : MonoBehaviour
{
    public GameObject happinessBar;
    public GameObject tenant;
    int happiness;
    // Start is called before the first frame update
    void Start()
    {
        //happinessBar = happinessBar.GetComponent<HappinessBar>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO so not constantly getting component
        happiness = happinessBar.GetComponent<HappinessBar>().happinessLevel;
        //happiness = happinessBar.happinessLevel;
        if (happiness <= 0){
            Debug.Log("Tenant leaving");
            Destroy(happinessBar, 1);
            Destroy(tenant, 1);
            
        }
        
    }
}
