using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bills : MonoBehaviour
{

    public GameObject countdown;
    public GameObject billsAlert;
    public GameObject billsAlert2;
    public GameObject levelHandler;
    public GameObject notificationBar;
    public GameObject moneyBar;
    public bool turnedOn;
    public float waitTime = 10f;

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
            notificationBar.GetComponent<NotificationBar>().SetBillsPending(
                countdownNum <= 20
            );
            if (countdownNum <= 0){
                countdownNum = 0;
                //lose game if bills are not paid
                levelHandler.GetComponent<LevelHandler>().loseLevel();
            }
        } else {
            // wait between events
            waitTimer += 0.01f;
            if (waitTimer >= waitTime){
                turnOn();
                waitTimer = 0.00f;
            }
        }
    }
    void reset(){
        turnedOn = false;
        gameTimer = 0f;
        countdownNum = 60;
        billsAlert.SetActive(false);
        billsAlert2.SetActive(false);
        countdown.SetActive(false);
    }
    void turnOn(){
        countdown.SetActive(true);
        billsAlert.SetActive(true);
        billsAlert2.SetActive(true);
        turnedOn = true;
    }
    void UpdateCountdown(){
        Text timeTextA = countdown.GetComponent<Text>();
        timeTextA.text = "" + countdownNum;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && turnedOn){
            // lose a little money
            moneyBar.GetComponent<MoneyBar>().subtractMoney(2);
           reset();

        }
    }
}
