using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public GameObject moneyBar;
    void Start()
    {
    }
    void Update()
    {
        // When money bar full, go to next level
        bool moneyFull = moneyBar.GetComponent<MoneyBar>().isFull();
        if (moneyFull){
            Debug.Log("Going to the next level");
            //TODO go to level transition scene here
        }
    }
}
