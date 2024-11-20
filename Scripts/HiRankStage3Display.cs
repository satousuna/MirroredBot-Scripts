using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HiRankStage3Display : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RankTM;
    void Start()
    {
        if(GManager.instance.HiRankStage3 == "S")
        {
        RankTM.text = "S";
        RankTM.color = new Color32(255, 255, 0 ,255);
        }
        else if(GManager.instance.HiRankStage3 == "A")
        {
        RankTM.text = "A";
        RankTM.color = new Color32(255, 0, 0 ,255);
        }
        else if(GManager.instance.HiRankStage3 == "B")
        {
        RankTM.text = "B";
        RankTM.color = new Color32(0, 191, 255 ,255);
        }
        else if(GManager.instance.HiRankStage3 == "C")
        {
        RankTM.text = "C";
        RankTM.color = new Color32(0, 255, 127 ,255);
        }
        else if(GManager.instance.HiRankStage3 == "D")
        {
        RankTM.text = "D";
        RankTM.color = new Color32(255, 140, 0 ,255);
        }
        else if(GManager.instance.HiRankStage3 == "E")
        {
        RankTM.text = "E";
        RankTM.color = new Color32(75, 0, 130 ,255);
        }
    }
}
