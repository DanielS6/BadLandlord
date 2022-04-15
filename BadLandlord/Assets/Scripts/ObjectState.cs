using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectState : MonoBehaviour
{
    const int PERFECT = 0, FINE = 1, BROKEN = 2;
    
    public bool broken;
    public int[] COSTS = { 20, 10, 0 }; // costs for new, quick, ignore
    public int[] SAFETIMES = { 5, 3 }; // time object is safe for after fix
    public int[] BREAKPROBS = { 3, 6 }; // probability of breaking 
                                        // pos int out of 100 (3 = 3% prob)

    public int curState; // PERFECT, FINE, or BROKEN
    public List<string> dropOptions = // options to send to dropdown menu
        new List<string> { "What will you do?",
                           "Buy new - $20",
                           "Quick Fix - $10",
                           "Ignore - $0" };
    public Animator anim;

   
    public const float TRIALFREQ = .25f; // freq of possibly breaking object
    public float gameTimer;
    private bool isSafe;

    private GameObject fixMenu; 
    private GameObject player;
    private Dropdown dropdown;
    private GameObject moneybar;
    private MoneyBar moneybarscript;
    private int curMoney; // currently only updated when needed
    private System.Random rnd = new System.Random();
    private List<string> curOptions = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        broken = false;
        curState = PERFECT;
        anim = gameObject.GetComponentInChildren<Animator>();

        // not sure why but currently declaration doesn't work above:
        BREAKPROBS[0] = 3;
        BREAKPROBS[1] = 6;

        isSafe = false;
        gameTimer = 0f;

        // find dropdown
        fixMenu = GameObject.FindGameObjectWithTag("ObjectFixMenu");
        dropdown = fixMenu.GetComponentInChildren<Dropdown>();
        dropdown.Hide();
        dropdown.ClearOptions();
        dropdown.onValueChanged.AddListener(delegate {
            HandleSelection(dropdown);
        });

        // find player
        player = GameObject.FindGameObjectWithTag("Player");

        // find moneybar
        moneybar = GameObject.FindGameObjectWithTag("MoneyBar");
        moneybarscript = moneybar.GetComponent<MoneyBar>();
    }

    // if object hasn't just been fixed (and is safe),
    // increase timer and call break trial upon TRIALFREQ interval
    private void FixedUpdate()
    {
        if (!isSafe)
        {
            gameTimer += 0.01f;
            if (gameTimer > TRIALFREQ)
            {
                ObjectBreakTrial();
                gameTimer = 0;
            }
        }
    }

    private void ObjectBreakTrial()
    {
        if (!broken)
        {
            int result = rnd.Next(100);
            broken = result < BREAKPROBS[curState];
            if (broken)
            {
                curState = BROKEN;
                ChangeObjectState();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Landlord interaction, give options for fixing
        if (other.CompareTag("Player"))
        {
            if (broken)
            {
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
                    } else
                    {
                        curOptions.Add(dropOptions[i + 1]);
                    }
                }


                ResetDropdown();
                // don't let landlord move until valid decision made
                player.SendMessage("DisableMovement"); 
            }
        }
    }

    // send options to dropdown menu
    private void ResetDropdown()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(curOptions);
        dropdown.Show();
    }

    private void HandleSelection(Dropdown changed)
    {
        int selected = changed.value - 1;
        if (selected == dropOptions.Count - 2) // if chose ignore
        {
            player.SendMessage("EnableMovement");
            changed.ClearOptions();
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
                curState = selected;
                moneybar.SendMessage("subtractMoney", cost);
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
    }

}
