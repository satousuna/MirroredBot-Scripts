using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountVerText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI VerCount;
    [SerializeField] private int VerCountInitial;
    [SerializeField] private int VerCounter;
    void Start()
    {
        VerCounter = VerCountInitial;
        VerCount.text = VerCountInitial.ToString();
        if(VerCounter == 0)
        {
            VerCount.color= new Color(1, 0, 0, 1);
        }
        else
        {
            VerCount.color= new Color(1, 1, 1, 1);
        }
    }
    public void ReduceVerCounter()
    {
        VerCounter = VerCounter - 1;
        VerCount.text = VerCounter.ToString();
        if(VerCounter == 0)
        {
            VerCount.color= new Color(1, 0, 0, 1);
        }
    }
    public void IncreaseVerCounter()
    {
        VerCounter = VerCounter + 1;
        VerCount.text = VerCounter.ToString();
        if(VerCounter == 1)
        {
            VerCount.color= new Color(1, 1, 1, 1);
        }
    }


    public int GetVerCounter()
    {
        return VerCounter;
    }
}
