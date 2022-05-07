using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectState : MonoBehaviour
{
    const int PERFECT = 0, FINE = 1, BROKEN = 2;
    
    public GameObject happinessBar;
    public float TRIALFREQ; // freq of possibly breaking object
    public bool broken;
    public int[] COSTS = { 20, 10, 0 }; // costs for new, quick, ignore
    public int[] SAFETIMES = { 10, 8 }; // time object is safe for after fix
    public int[] BREAKPROBS = { 10, 20 }; // probability of breaking 
                                        // pos int out of 100 (10 = 10% prob)
        //BREAKPROBS INIT NOT WORKING IDK WHY, ASSIGN VALUES IN INSPECTOR

    public int[] HAPPINESSEFFECT = { 1, 0, -1 }; // 1 = +1 happy, -1 = -1 happy
    public int FIXBEFORE = 15; // time to fix object before happiness dec

    public int curState; // PERFECT, FINE, or BROKEN
    public List<string> dropOptions = // options to send to dropdown menu
        new List<string> { "What will you do?",
                           "Buy new - $20",
                           "Quick Fix - $10",
                           "Ignore - $0" };

    public float ALERTFREQ = .25f; // pause time for alert flashing
    public float gameTimer;
    private bool isSafe;
    private bool alertOn;

    private GameObject fixMenu; 
    private GameObject player;
    private Dropdown dropdown;
    private GameObject moneybar;
    private MoneyBar moneybarscript;
    private SpriteRenderer interactPrompt;
    private SpriteRenderer alert;
    private Animator anim;

    private int curMoney; // currently only updated when needed
    private System.Random rnd = new System.Random();
    private List<string> curOptions = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        broken = false;
        curState = PERFECT;
        anim = gameObject.GetComponentInChildren<Animator>();

        // e to interact prompt
        interactPrompt =
            this.transform.Find("e_to_interact").GetComponent<SpriteRenderer>();
        interactPrompt.enabled = false;

        // object broken alert
        alert = this.transform.Find("AlertTEMP1").GetComponent<SpriteRenderer>();
        alert.enabled = false;
        alertOn = false;


        isSafe = false;
        gameTimer = 0f;

        // find dropdown
        fixMenu = GameObject.FindGameObjectWithTag("ObjectFixMenu");
        dropdown = fixMenu.GetComponentInChildren<Dropdown>();
        dropdown.Hide();
        dropdown.ClearOptions();
        

        // find player
        player = GameObject.FindGameObjectWithTag("Player");

        // find moneybar
        moneybar = GameObject.FindGameObjectWithTag("MoneyBar");
        moneybarscript = moneybar.GetComponent<MoneyBar>();
    }

    private void Update()
    {
        if (interactPrompt.enabled && Input.GetKeyDown(KeyCode.E))
        {
            //hide prompt;

            interactPrompt.enabled = false;

            // update curMoney
            curMoney = moneybarscript.current;

            // Make new temporary options list based on curMoney
            curOptions.Clear();
            curOptions.Add(dropOptions[0]);
            for (int i = 0; i < COSTS.Length; i++)
            {
                if (COSTS[i] > curMoney) // not enough money
                {
                    curOptions.Add("not enough $: " + dropOptions[i + 1]);
                }
                else
                {
                    curOptions.Add(dropOptions[i + 1]);
                }
            }

            ResetDropdown();
            // don't let landlord move until valid decision made
            player.SendMessage("DisableMovement");
        }
    }

    private void FixedUpdate()
    {
        // if object hasn't just been fixed (and is safe),
        // increase timer and call break trial upon TRIALFREQ interval
        //if (!isSafe && !broken)
        //{
        //    gameTimer += 0.01f;
        //    if (gameTimer > TRIALFREQ)
        //    {
        //        ObjectBreakTrial();
        //        gameTimer = 0;
        //    }
        //}

        // if object breaks and hasn't been ignored, flash alert
        if (alertOn)
        {
            gameTimer += 0.01f;
            if (gameTimer > ALERTFREQ)
            {
                alert.enabled = !alert.enabled;
                gameTimer = 0;
            }
        } else
        {
            alert.enabled = false;
        }
    }

    public void ObjectBreakTrial()
    {
        int result = rnd.Next(100);
        broken = result < BREAKPROBS[curState];
        if (broken)
        {
            curState = BROKEN;
            ChangeObjectState();
            StartCoroutine(TimeToFix());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Landlord interaction, give options for fixing
        if (other.CompareTag("Player") && broken)
        {
            //readyToInteract = true;
            interactPrompt.enabled = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // hide interact prompt
        if (other.CompareTag("Player"))
        {
            interactPrompt.enabled = false;
        }
    }

    // send options to dropdown menu, add listener
    private void ResetDropdown()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(curOptions);
        dropdown.Show();
        dropdown.onValueChanged.AddListener(HandleSelection);
    }

    // remove listener, handle dropdown input
    private void HandleSelection(int arg)
    {
        dropdown.onValueChanged.RemoveListener(HandleSelection);
        int selected = dropdown.value - 1;
        if (selected == dropOptions.Count - 2) // if chose ignore
        {
            alertOn = false;
            player.SendMessage("EnableMovement");
            dropdown.ClearOptions();
        }
        else
        {
            int cost = COSTS[selected];

            // not enough money: keep listening for valid answer
            if (cost > moneybarscript.current)
            {
                ResetDropdown();
            }
            // valid answer: update accordingly
            else
            {
                alertOn = false;
                curState = selected;
                moneybar.SendMessage("subtractMoney", cost);
                happinessBar.SendMessage("addHappy",
                                         HAPPINESSEFFECT[selected]);
                StartCoroutine(Fix());
            }
        }
    }

    // Play landlord fix animation, update object state
    // Pre-req: curState should already be updated to state to change to
    IEnumerator Fix()
    {
        isSafe = true;
        player.SendMessage("FixAnimEnter");
        yield return new WaitForSeconds(0.5f);
        player.SendMessage("FixAnimExit");
        
        player.SendMessage("EnableMovement"); // allow player movement again
        ChangeObjectState();
        dropdown.ClearOptions();

        yield return new WaitForSeconds(SAFETIMES[curState]);
        isSafe = false;
    }

    // Updates animator and "broken" bool
    // Pre-req: curState should already be updated to state to change to
    private void ChangeObjectState()
    {
        anim.SetInteger("curState", curState);

        // update broken bool to correct value
        broken = curState == BROKEN ? true : false;
        anim.SetBool("broken", broken);

        if (broken)
        {
            alertOn = true;
        }
    }

    // called once object breaks,
    // if not fixed within specified time, happiness decreases
    IEnumerator TimeToFix()
    {
        yield return new WaitForSeconds(FIXBEFORE);
        if (broken)
        {
            happinessBar.SendMessage("addHappy", HAPPINESSEFFECT[BROKEN]);
        }
    }
}