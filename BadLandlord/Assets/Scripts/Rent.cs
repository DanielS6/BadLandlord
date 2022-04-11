using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rent : MonoBehaviour
{
    public GameObject date;
    public GameObject money;
    public GameObject tenant;

    int storedMonth;
    int curMonth;

    // Start is called before the first frame update
    void Start()
    {
        storedMonth = 0;
        curMonth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        curMonth = date.GetComponent<Date>().month;
        if (curMonth != storedMonth){
            //For every tenant in building, have them offer up rent
            tenant.GetComponent<TenantLogic>().offerRent();
            storedMonth = curMonth;
        }
    }
}
