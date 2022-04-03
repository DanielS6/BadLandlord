using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessBar : MonoBehaviour
{
    public int happinessLevel;
    public GameObject happy1;
    public GameObject happy2;
    public GameObject happy3;
    public GameObject happy4;
    // Start is called before the first frame update
    void Start()
    {
        happinessLevel = 4;
        
    }

    // Update is called once per frame
    void Update()
    {
        showAtLevel(1, happy1);
        showAtLevel(2, happy2);
        showAtLevel(3, happy3);
        showAtLevel(4, happy4);
    }
    
    void showAtLevel(int level, GameObject happyArt)
    {
        if (happinessLevel >= level){
            happyArt.SetActive(true);
        } else {
            happyArt.SetActive(false);
        }
    }
}
