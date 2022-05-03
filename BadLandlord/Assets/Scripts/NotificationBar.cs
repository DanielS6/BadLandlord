using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationBar : MonoBehaviour
{
    public GameObject notificationText;

    void Start()
    {
        notificationText.GetComponent<Text>().text = "";
    }
    
    /* PUBLIC FUNCTIONS */
    public void display(string textToDisplay){
        Text curMessage = notificationText.GetComponent<Text>();
        curMessage.text = textToDisplay;

    }
}
