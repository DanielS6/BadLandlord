using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectState : MonoBehaviour
{
    public bool broken;
    const int PERFECT = 0, FINE = 1, BROKEN = 2;
    public int[] COSTS = { 20, 10, 0 };
    public int curState;
    public List<string> dropOptions =
        new List<string> { "What will you do?", "Buy new - $20", "Quick Fix - $10", "Ignore - $0" };
    private GameObject fixMenu;
    private GameObject player;
    private Dropdown dropdown;
    private GameObject moneybar;
    private MoneyBar moneybarscript;
    private int curMoney;
    public Animator anim;

    // times for perfect, fine
    public int[] BREAKTIMES = new int[2];


    // Start is called before the first frame update
    void Start()
    {
        broken = false;
        curState = PERFECT;
        anim = gameObject.GetComponentInChildren<Animator>();

        // find dropdown
        fixMenu = GameObject.FindGameObjectWithTag("ObjectFixMenu");
        dropdown = fixMenu.GetComponentInChildren<Dropdown>();
        dropdown.Hide();
        dropdown.ClearOptions();

        // find player
        player = GameObject.FindGameObjectWithTag("Player");

        moneybar = GameObject.FindGameObjectWithTag("MoneyBar");
        moneybarscript = moneybar.GetComponent<MoneyBar>();

        StartCoroutine(WaitBreak());

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator WaitBreak()
    {
        yield return new WaitForSeconds(BREAKTIMES[curState]);
        curState = BROKEN;
        ChangeObjectState();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (broken)
            {
                curMoney = moneybarscript.current;
                List<string> curOptions = new List<string>();
                curOptions.Add(dropOptions[0]); 
                for (int i = 0; i < COSTS.Length; i++)
                {
                    if (COSTS[i] > curMoney)
                    {
                        curOptions.Add("not enough $: " + dropOptions[i + 1]);
                    } else
                    {
                        curOptions.Add(dropOptions[i + 1]);
                    }
                }

                dropdown.AddOptions(curOptions);
                dropdown.Show();
                player.SendMessage("DisableMovement");
                ListenForSelection();
            }
        }
    }

    private void ListenForSelection()
    {
        dropdown.onValueChanged.AddListener(delegate {
            int selected = dropdown.value - 1;
            int cost = COSTS[selected];
            if (cost > moneybarscript.current) // not enough money
            {
                dropdown.Show();
                ListenForSelection();
            }
            else
            {
                curState = dropdown.value - 1;
                moneybar.SendMessage("subtractMoney", cost);
                StartCoroutine(Fix());
            }

        });
    }

    IEnumerator Fix()
    {
        if (curState != BROKEN)
        {
            player.SendMessage("FixAnimEnter");
            yield return new WaitForSeconds(1);
            player.SendMessage("FixAnimExit");
        }
        
        player.SendMessage("EnableMovement");
        ChangeObjectState();
        dropdown.ClearOptions();
    }

    // Changes object state (perfect, fine, broken)
    private void ChangeObjectState()
    {
        anim.SetInteger("curState", curState);

        // update broken bool to correct value
        broken = curState == BROKEN ? true : false;
        anim.SetBool("broken", broken);

        if (curState == PERFECT)
        {
            StartCoroutine(WaitBreak());
        }
        else if (curState == FINE)
        {
            StartCoroutine(WaitBreak());
        }
    }

}
