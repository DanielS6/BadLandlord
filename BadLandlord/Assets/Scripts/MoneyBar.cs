using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode()]

public class MoneyBar : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image mask;

    void Start()
    {
        current = 0;
    }

    /* PUBLIC FUNCTIONS */
    public void addMoney(int amount){
        current += amount;
    }
    public void subtractMoney(int amount){
        int difference = current - amount;
        if (difference > 0){
            current = difference;
        } else {
            current = 0;
        }
    }

    /* PRIVATE FUNCTIONS */
    void Update()
    {
        GetCurrentFill();
    }
    
    void GetCurrentFill(){
        float fillAmount = (float) current / (float) maximum;
        mask.fillAmount = fillAmount;
    }
    

}
