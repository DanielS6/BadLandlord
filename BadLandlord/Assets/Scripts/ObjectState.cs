using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectState : MonoBehaviour
{
    public bool active;
    public bool broken;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray = new Sprite[3];
    const int PERFECT = 0, FINE = 1, BROKEN = 2;
    public int curState;
    List<string> dropOptions =
        new List<string> { "What will you do?", "Buy new - $20", "Quick Fix - $10", "Ignore - $0" };
    public Dropdown objectMenu;

    // times for perfect, fine
    public int[] BREAKTIMES = new int[2];


    // Start is called before the first frame update
    void Start()
    {
        active = true;
        broken = false;
        curState = PERFECT;
        spriteRenderer.sprite = spriteArray[curState];

        objectMenu.Hide();
        objectMenu.ClearOptions();

        StartCoroutine(WaitBreak());

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator WaitBreak()
    {
        yield return new WaitForSeconds(BREAKTIMES[curState]);
        ChangeObjectState(BROKEN);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (broken)
            {
                objectMenu.AddOptions(dropOptions);
                objectMenu.Show();
            }
        }
    }

    public void Fix()
    {
        ChangeObjectState(objectMenu.value - 1);
        objectMenu.ClearOptions();
    }

    // Changes object state (perfect, fine, broken)
    private void ChangeObjectState(int newState)
    {
        curState = newState;

        // update sprite to correct image
        spriteRenderer.sprite = spriteArray[curState];

        // update broken bool to correct value
        broken = newState == BROKEN ? true : false;

        if (newState == PERFECT)
        {
            StartCoroutine(WaitBreak());
        } else if (newState == FINE)
        {
            StartCoroutine(WaitBreak());
        }
    }

}
