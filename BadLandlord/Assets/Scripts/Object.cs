using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object : MonoBehaviour
{
    public bool active;
    public bool broken;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray = new Sprite[3];
    const int PERFECT = 0, FINE = 1, BROKEN = 2;
    public int curState;

    // times for perfect, fine
    public int[] BREAKTIMES = new int[2];

    //Dropdown m_Dropdown;
    //public Text m_Text; 

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        broken = false;
        curState = PERFECT;
        spriteRenderer.sprite = spriteArray[curState];

        StartCoroutine(PerfectWaitBreak());

        //m_Dropdown = GetComponentInChildren<Dropdown>();
        ////Add listener for when the value of the Dropdown changes, to take action
        //m_Dropdown.onValueChanged.AddListener(delegate
        //{
        //    DropdownValueChanged(m_Dropdown);
        //});
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && broken)
        {
            ChangeObjectState(PERFECT);

            StartCoroutine(PerfectWaitBreak());
        } 
    }

    IEnumerator PerfectWaitBreak()
    {
        yield return new WaitForSeconds(BREAKTIMES[PERFECT]);
        ChangeObjectState(BROKEN);
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log("player trigger");
    //        if (broken)
    //        {
    //            Debug.Log("should change idk");
    //            ChangeObjectState(PERFECT);
    //        }
    //    }
    //}

    // Changes object state (perfect, fine, broken)
    void ChangeObjectState(int newState)
    {
        curState = newState;

        // update sprite to correct image
        spriteRenderer.sprite = spriteArray[curState];

        // update broken bool to correct value
        broken = newState == BROKEN ? true : false;
    }

    //Ouput the new value of the Dropdown into Text
    //void DropdownValueChanged(Dropdown change)
    //{
    //    Debug.Log(m_Dropdown.value);
    //}


}
