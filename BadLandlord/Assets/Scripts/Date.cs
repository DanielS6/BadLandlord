using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Date : MonoBehaviour
{
    public GameObject dateText;

    public float gameTimer = 0f;
    public int month;

    void Start()
    {
        month = 0;
        updateDate();
    }
    
    void FixedUpdate()
    {
        gameTimer += 0.01f;
        if (gameTimer > 1f){
            month++;
            updateDate();
            gameTimer = 0;
        }
    }
    void updateDate(){
        Text dateTextA = dateText.GetComponent<Text>();
        dateTextA.text = "" + month;
    }
}
