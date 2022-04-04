using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object : MonoBehaviour
{
    public bool active;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray = new Sprite[3];
    const int PERFECT = 0, FINE = 1, BROKEN = 2;
    public int curState;

    // times for perfect, fine
    public int[] BREAKTIMES = new int[2];

    Dropdown m_Dropdown;
    //public Text m_Text; 

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        curState = PERFECT;
        spriteRenderer.sprite = spriteArray[curState];

        m_Dropdown = GetComponentInChildren<Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(m_Dropdown);
        });
    }


    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(Dropdown change)
    {
        Debug.Log(m_Dropdown.value);
    }

        // Update is called once per frame
        void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space");
            m_Dropdown.Show();
        }
    }

    // Changes object state (perfect, fine, broken)
    void ChangeObjectState(int newState)
    {
        curState = newState;
        spriteRenderer.sprite = spriteArray[curState];
    }
}
