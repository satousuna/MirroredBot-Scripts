using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountHorText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HorCount;
    [SerializeField] private int HorCountInitial;
    [SerializeField] private int HorCounter;
    void Start()
    {
        HorCounter = HorCountInitial;
        HorCount.text = HorCountInitial.ToString();
        if(HorCounter == 0)
        {
            HorCount.color= new Color(1, 0, 0, 1);
        }
        else
        {
            HorCount.color= new Color(1, 1, 1, 1);
        }
    }
    public void ReduceHorCounter()
    {
        HorCounter = HorCounter - 1;
        HorCount.text = HorCounter.ToString();
        if(HorCounter == 0)
        {
            HorCount.color= new Color(1, 0, 0, 1);
        }
    }
    public void IncreaseHorCounter()
    {
        HorCounter = HorCounter + 1;
        HorCount.text = HorCounter.ToString();
        if(HorCounter == 1)
        {
            HorCount.color= new Color(1, 1, 1, 1);
        }
    }


    public int GetHorCounter()
    {
        return HorCounter;
    }
}
