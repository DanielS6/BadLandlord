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
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }
    
    void GetCurrentFill(){
        float fillAmount = (float) current / (float) maximum;
        mask.fillAmount = fillAmount;
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
}
