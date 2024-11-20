using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ResultStage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ResultTM;
    private String ResulttextUI;
    void Start()
    {
        if (GManager.instance.StageNum == 15)
        {
            ResulttextUI =("STAGE 1");
        }
        else if (GManager.instance.StageNum == 24)
        {
            ResulttextUI =("STAGE 2");
        }
        else if (GManager.instance.StageNum == 34)
        {
            ResulttextUI =("STAGE 3");
        }
        else if (GManager.instance.StageNum == 44)
        {
            ResulttextUI =("STAGE 4");
        }
        ResultTM.text = ResulttextUI;
    }
}
