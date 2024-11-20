using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankDisplay : MonoBehaviour
{
    [SerializeField] GameObject ResultManager;
    private ResultManager resultmanager;
    [SerializeField] private TextMeshProUGUI RankTM;
    private void Start() 
    {
        resultmanager = ResultManager.GetComponent<ResultManager>();
        StartCoroutine ("Rankroulette");
    }
 
    IEnumerator Rankroulette()
    {
        while (!resultmanager.GetResultRankSet())
        {
        RankTM.text = "S";
        RankTM.color = new Color32(255, 255, 0 ,255);
        yield return new WaitForSeconds(0.05f);
        RankTM.text = "A";
        RankTM.color = new Color32(255, 0, 0 ,255);
        yield return new WaitForSeconds(0.05f);
        RankTM.text = "B";
        RankTM.color = new Color32(0, 191, 255 ,255);
        yield return new WaitForSeconds(0.05f);
        RankTM.text = "C";
        RankTM.color = new Color32(0, 255, 127 ,255);
        yield return new WaitForSeconds(0.05f);
        RankTM.text = "D";
        RankTM.color = new Color32(255, 140, 0 ,255);
        yield return new WaitForSeconds(0.05f);
        RankTM.text = "E";
        RankTM.color = new Color32(75, 0, 130 ,255);
        yield return new WaitForSeconds(0.05f);
        }
        
    }
}
