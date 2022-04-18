using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            nextLevel();
        }
    }
    public void nextLevel(){
        SceneManager.LoadScene("WinLevel");
    }
    public void loseLevel(){
        SceneManager.LoadScene("LoseLevel");
    }
}
