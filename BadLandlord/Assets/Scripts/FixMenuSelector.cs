using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixMenuSelector : MonoBehaviour
{
    [SerializeField] private Dropdown m_Dropdown; // wire this in Inspector
    // Start is called before the first frame update

    public void OnDropdownChanged()
    {
        Debug.Log(m_Dropdown.value);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
