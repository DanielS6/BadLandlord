using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bills : MonoBehaviour
{
    public GameObject countdown;
    public GameObject billsAlert;
    public GameObject levelHandler;
    public bool turnedOn;
    float gameTimer;
    float waitTimer;
    int countdownNum;

    void Start()
    {
        reset();
    }

    void FixedUpdate()
    {
        if (turnedOn){
            gameTimer += 0.01f;
            if (gameTimer >= 1f){
                countdownNum -= 1;
                gameTimer = 0;
                UpdateCountdown();
            }
            if (countdownNum <= 0){
                countdownNum = 0;
                //lose game if bills are not paid
                levelHandler.GetComponent<LevelHandler>().loseLevel();
            }
        } else {
            // wait between events
            waitTimer += 0.01f;
            if (waitTimer >= 10f){
                turnOn();
                waitTimer = 0.00f;
            }
        }
    }
    void reset(){
        Debug.Log("bills resetting");
        turnedOn = false;
        gameTimer = 0f;
        countdownNum = 100;
        billsAlert.SetActive(false);
        countdown.SetActive(false);
    }
    void turnOn(){
        countdown.SetActive(true);
        billsAlert.SetActive(true);
        turnedOn = true;
    }
    void UpdateCountdown(){
        Text timeTextA = countdown.GetComponent<Text>();
        timeTextA.text = "" + countdownNum;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("bills colliding w/player");
        if (other.CompareTag("Player") && turnedOn){
           reset();
            
        }
    }
}
