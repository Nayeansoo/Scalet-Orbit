using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUI : MonoBehaviour
{
    public GameObject help_1;
    public GameObject help_2;
    public GameObject help_3;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void rightClik()
    {
        if (help_1.activeSelf == true && help_2.activeSelf == false && help_3.activeSelf == false)
        {
            help_1.SetActive(false);
            help_2.SetActive(true);
        }
        else if (help_1.activeSelf == false && help_2.activeSelf == true && help_3.activeSelf == false)
        {
            help_2.SetActive(false);
            help_3.SetActive(true);
        }
    }

    public void leftClick()
    {
        if (help_1.activeSelf == false && help_2.activeSelf == false && help_3.activeSelf == true)
        {
            help_2.SetActive(true);
            help_3.SetActive(false);
        }
        else if (help_1.activeSelf == false && help_2.activeSelf == true && help_3.activeSelf == false)
        {
            help_1.SetActive(true);
            help_2.SetActive(false);
        }
    }
}
