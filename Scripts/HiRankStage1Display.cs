using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class HiRankStage1Display : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RankTM;
    void Start()
    {
        if(GManager.instance.HiRankStage1 == "S")
        {
        RankTM.text = "S";
        RankTM.color = new Color32(255, 255, 0 ,255);
        }
        else if(GManager.instance.HiRankStage1 == "A")
        {
        RankTM.text = "A";
        RankTM.color = new Color32(255, 0, 0 ,255);
        }
        else if(GManager.instance.HiRankStage1 == "B")
        {
        RankTM.text = "B";
        RankTM.color = new Color32(0, 191, 255 ,255);
        }
        else if(GManager.instance.HiRankStage1 == "C")
        {
        RankTM.text = "C";
        RankTM.color = new Color32(0, 255, 127 ,255);
        }
        else if(GManager.instance.HiRankStage1 == "D")
        {
        RankTM.text = "D";
        RankTM.color = new Color32(255, 140, 0 ,255);
        }
        else if(GManager.instance.HiRankStage1 == "E")
        {
        RankTM.text = "E";
        RankTM.color = new Color32(75, 0, 130 ,255);
        }
        else return;
    }
}
