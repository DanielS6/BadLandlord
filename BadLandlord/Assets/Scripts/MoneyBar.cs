using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//[ExecuteInEditMode()]

public class MoneyBar : MonoBehaviour
{
    public GameObject moneyText;
    public int[] maximums = { 100, 150, 200, 250 };
    public int current;
    public Image mask;

    private int maximum;
    private string[] levelNames = { "Level1", "Level2", "level3", "levelfour" };
    private string curLevel;


    void Start()
    {
        current = 0;

        curLevel = SceneManager.GetActiveScene().name;
        for (int i = 0; i < levelNames.Length; i++)
        {
            if (levelNames[i] == curLevel)
            {
                maximum = maximums[i];
                continue;
            }
        }
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
    public bool isFull(){
        if (current >= maximum){
            return true;
        } else {
            return false;
        }
    }

    /* PRIVATE FUNCTIONS */
    void Update()
    {
        GetCurrentFill();
        updateMoneyText();
    }
    void updateMoneyText(){
        Text moneyTextA = moneyText.GetComponent<Text>();
        moneyTextA.text = "$ " + current + " / " + maximum;        
    }
    void GetCurrentFill(){
        float fillAmount = (float) current / (float) maximum;
        mask.fillAmount = fillAmount;
    }
    

}
