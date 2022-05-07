using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappinessBar : MonoBehaviour
{

    // connect to notification bar to notify when tenant too unhappy
    public GameObject notificationBar;
    public GameObject levelHandler;
    // 4 levels of happyness, each with a different image
    // (there are 6 images available if we want to use them all later)
    public Sprite faceImage1;
    public Sprite faceImage2;
    public Sprite faceImage3;
    public Sprite faceImage4;

    // Images as an array for ease of access
    private Sprite[] faceImages = new Sprite[4];

    // Object holding the current displayed face
    public Image displayFace;

    public int happinessLevel = 4;
    private int maxHappinessLevel = 4;

    void Start() {
        happinessLevel = 4;
        faceImages[0] = faceImage1;
        faceImages[1] = faceImage2;
        faceImages[2] = faceImage3;
        faceImages[3] = faceImage4;
    }

    /* PUBLIC FUNCTIONS */
    public void addHappy(int amount){
        int newAmount = happinessLevel + amount;
        if (newAmount < maxHappinessLevel){
            happinessLevel = newAmount;
        } else {
            happinessLevel = maxHappinessLevel;
        }
    }
    public void subtractHappy(int amount){
        happinessLevel -= amount;
    }

    /* PRIVATE FUNCTIONS */
    void Update() {
        // Don't try to access invalid array indexes
        if (happinessLevel >= 1 && happinessLevel <= 4) {
            // Level is [1, 4] but array indexes are [0, 3]
            displayFace.sprite = faceImages[ happinessLevel - 1 ];
        }
        if (happinessLevel <= 1){
            notificationBar.GetComponent<NotificationBar>().display(
                "A tenant is unhappy and about to leave!");
            //lose if tenant is unhappy
            if (happinessLevel <= 0){
                levelHandler.GetComponent<LevelHandler>().loseLevel();
            }
        }

    }

}
