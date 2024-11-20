using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ResultManager : MonoBehaviour
{
    int ResultDrumroll = 0;
    [SerializeField] private GameObject ResultText;
    [SerializeField] private GameObject GemText;
    [SerializeField] private GameObject RetryText;
    [SerializeField] private GameObject TimeText;
    [SerializeField] private GameObject RankText;
    [SerializeField] private GameObject Gem;
    [SerializeField] private GameObject Gem_1;
    [SerializeField] private GameObject Gem_2;
    [SerializeField] private GameObject RetryResult;
    [SerializeField] private GameObject TimeResult;
    [SerializeField] private TextMeshProUGUI TimeTM;
    [SerializeField] private TextMeshProUGUI RetryTM;
    [SerializeField] private GameObject RankResult;
    [SerializeField] private GameObject WhiteFlashImage;
    [SerializeField] private GameObject ButtonManager;
    [SerializeField] private TextMeshProUGUI RankTM;
    [SerializeField] private Animator animGem = null;
    [SerializeField] private Animator animGem1 = null;
    [SerializeField] private Animator animGem2 = null;
    [SerializeField] private ParticleSystem ParticlesGem;
    [SerializeField] private ParticleSystem ParticlesGem1;
    [SerializeField] private ParticleSystem ParticlesGem2;
    [SerializeField] private Animator animWhiteFlash;
    private String timerTextUI;
    private String RetryTextUI;
    private String RankTextUI;
    private string timeString;
    private int RankScore;
    private bool DidResult;
    private bool ResultRankSet = false;
    [SerializeField] GameObject RankDisplay;
    void Start()
    {
        timerTextUI = WriteTimeFormat(GManager.instance.ClearTime);   
        Resultdecision();
        StartCoroutine ("ResultAnnounce");
    }
    IEnumerator ResultAnnounce()
    {
        yield return new WaitForSeconds(1.5f);
        ResultText.SetActive(true);//RESULT表示
        SoundManager.Instance.PlaySE(SESoundData.SE.ResultHead);//リザルト効果音
        yield return new WaitForSeconds(1.5f);
        GemText.SetActive(true);//GEM表示
        Gem.SetActive(true);
        Gem_1.SetActive(true);
        Gem_2.SetActive(true);
        SoundManager.Instance.PlaySE(SESoundData.SE.ResultHead);//リザルト効果音
        if(GManager.instance.GemCount > 0)//GEM獲得数に応じアニメーションとパーティクル表示
        {
            yield return new WaitForSeconds(0.7f);
            SoundManager.Instance.PlaySE(SESoundData.SE.ItemGet);
            animGem.SetTrigger("GemGet");
            ParticlesGem.Play();
        }
        if(GManager.instance.GemCount > 1)
        {
            yield return new WaitForSeconds(0.7f);
            SoundManager.Instance.PlaySE(SESoundData.SE.ItemGet);
            animGem1.SetTrigger("GemGet");//GEMアニメーションとパーティクル表示
            ParticlesGem1.Play();
        }
        if(GManager.instance.GemCount > 2)
        {    
            yield return new WaitForSeconds(0.7f);
            SoundManager.Instance.PlaySE(SESoundData.SE.ItemGet);
            animGem2.SetTrigger("GemGet");//GEMアニメーションとパーティクル表示
            ParticlesGem2.Play();
        }
        yield return new WaitForSeconds(0.7f);
        RetryText.SetActive(true);//Retry表示
        SoundManager.Instance.PlaySE(SESoundData.SE.ResultHead);//リザルト効果音
        yield return new WaitForSeconds(0.7f);
        RetryResult.SetActive(true);//Retry数表示
        SoundManager.Instance.PlaySE(SESoundData.SE.ResultHead);//リザルト効果音
        RetryTextUI = GManager.instance.DeathCount.ToString();
        RetryTM.text = RetryTextUI;
        yield return new WaitForSeconds(0.7f);
        TimeText.SetActive(true); //TIME表示
        SoundManager.Instance.PlaySE(SESoundData.SE.ResultHead);//リザルト効果音
        yield return new WaitForSeconds(0.7f);
        TimeResult.SetActive(true);//TIME計測結果表示
        SoundManager.Instance.PlaySE(SESoundData.SE.ResultHead);//リザルト効果音
        TimeTM.text = timerTextUI; 
        yield return new WaitForSeconds(0.7f);
        RankText.SetActive(true);//RANK表示
        SoundManager.Instance.PlaySE(SESoundData.SE.ResultHead);//リザルト効果音
        yield return new WaitForSeconds(0.7f);   
        RankResult.SetActive(true);
        StartCoroutine(Rankroulette());//RANK結果切り替えドラムロール
        SoundManager.Instance.PlaySE(SESoundData.SE.DrumRoll);
    }

    IEnumerator ResultAppear()
    {
        DidResult = true;
        RankSet();//RANK結果表示　花火パーティクル表示
        SoundManager.Instance.PlaySE(SESoundData.SE.ResultRank);//ランク効果音
        HiRankSet();
        animWhiteFlash.SetTrigger("WhiteFlashActivate");
        yield return new WaitForSeconds(2f);
        ButtonManager.SetActive(true);//RESTART、EXIT表示
    }
    private void Update() 
    {
        if(ResultDrumroll == 5&&!DidResult)
        {
            StartCoroutine ("ResultAppear");
        }
        
    }
    private string WriteTimeFormat(float timer)//floatの値をタイム表記の文字列で返す
    {
        timeString = string.Format("{0:D2}:{1:D2}:{2:D2}",
        (int)timer / 60,
        (int)timer % 60,
        (int)(timer * 100) % 60);
        return timeString;
    }
    private void Resultdecision()
    {
        if (GManager.instance.GemCount == 3)
        RankScore += 150;
        else if(GManager.instance.GemCount == 2)
        RankScore += 100;
        else if(GManager.instance.GemCount == 1)
        RankScore += 50;

        if (GManager.instance.DeathCount >= 0 && GManager.instance.DeathCount < 10)
        RankScore += 150;
        else if(GManager.instance.DeathCount >= 10 && GManager.instance.DeathCount < 20)
        RankScore += 100;
        else if(GManager.instance.DeathCount >= 20 && GManager.instance.DeathCount < 30)
        RankScore += 50;

        if (GManager.instance.StageNum == 15)
        {
            if(GManager.instance.ClearTime < 55)
            {
                RankScore += 150;
            }
            else if(GManager.instance.ClearTime >= 55 && GManager.instance.ClearTime < 85)
            {
                RankScore += 100;
            }
            else if(GManager.instance.ClearTime >= 85 && GManager.instance.ClearTime < 135)
            {
                RankScore += 50;
            }
        }
        if (GManager.instance.StageNum == 24)
        {
            if(GManager.instance.ClearTime < 80)
            {
                RankScore += 150;
            }
            else if(GManager.instance.ClearTime >= 80 && GManager.instance.ClearTime < 120)
            {
                RankScore += 100;
            }
            else if(GManager.instance.ClearTime >= 120 && GManager.instance.ClearTime < 300)
            {
                RankScore += 50;
            }
        }
        if (GManager.instance.StageNum == 34)
        {
            if(GManager.instance.ClearTime < 120)
            {
                RankScore += 150;
            }
            else if(GManager.instance.ClearTime >= 120 && GManager.instance.ClearTime < 400)
            {
                RankScore += 100;
            }
            else if(GManager.instance.ClearTime >= 400 && GManager.instance.ClearTime < 600)
            {
                RankScore += 50;
            }
        }
        if (GManager.instance.StageNum == 44)
        {
            if(GManager.instance.ClearTime < 120)
            {
                RankScore += 150;
            }
            else if(GManager.instance.ClearTime >= 120 && GManager.instance.ClearTime < 400)
            {
                RankScore += 100;
            }
            else if(GManager.instance.ClearTime >= 400 && GManager.instance.ClearTime < 600)
            {
                RankScore += 50;
            }
        }
    }

    private void RankSet()
    {
        if (RankScore == 450)
        {
            RankTM.text = "S";
            RankTM.color = new Color32(255, 255, 0 ,255);
        }
        if (RankScore == 400)
        {
            RankTM.text = "A";
            RankTM.color = new Color32(255, 0, 0 ,255);
        }
        if (RankScore < 400 && RankScore >= 300)
        {
            RankTM.text = "B";
            RankTM.color = new Color32(0, 191, 255 ,255);
        }
        if (RankScore < 300 && RankScore >= 200)
        {
            RankTM.text = "C";
            RankTM.color = new Color32(0, 255, 127 ,255);
        }
        if (RankScore < 200 && RankScore >= 100)
        {
            RankTM.text = "D";
            RankTM.color = new Color32(255, 140, 0 ,255);
        }
        if (RankScore < 50)
        {
            RankTM.text = "E";
            RankTM.color = new Color32(75, 0, 130 ,255);
        }
    }
    public bool GetResultRankSet()
    {
        return ResultRankSet;
    }
    private void HiRankSet()
    {
        if(GManager.instance.StageNum == 15)
        {   
            if(RankTM.text == "S"&&(GManager.instance.RankNumber1 < 5||GManager.instance.RankNumber1 == null))
            {
                GManager.instance.HiRankStage1 = "S";
                GManager.instance.RankNumber1 = 5;
            }
            if(RankTM.text == "A"&&(GManager.instance.RankNumber1 < 4||GManager.instance.RankNumber1 == null))
            {
                GManager.instance.HiRankStage1 = "A";
                GManager.instance.RankNumber1 = 4;
            }
            if(RankTM.text == "B"&&(GManager.instance.RankNumber1 < 3||GManager.instance.RankNumber1 == null))
            {
                GManager.instance.HiRankStage1 = "B";
                GManager.instance.RankNumber1 = 3;
            }
            if(RankTM.text == "C"&&(GManager.instance.RankNumber1 < 2||GManager.instance.RankNumber1 == null))
            {
                GManager.instance.HiRankStage1 = "C";
                GManager.instance.RankNumber1 = 2;
            }
            if(RankTM.text == "D"&&(GManager.instance.RankNumber1 < 1||GManager.instance.RankNumber1 == null))
            {
                GManager.instance.HiRankStage1 = "D";
                GManager.instance.RankNumber1 = 1;
            }
            if(RankTM.text == "E"&&(GManager.instance.RankNumber1 == null))
            {
                GManager.instance.HiRankStage1 = "E";
                GManager.instance.RankNumber1 = 0;
            }
        }
        else if(GManager.instance.StageNum == 24)
        {   
            if(RankTM.text == "S"&&(GManager.instance.RankNumber2 < 5||GManager.instance.RankNumber2 == null))
            {
                GManager.instance.HiRankStage2 = "S";
                GManager.instance.RankNumber2 = 5;
            }
            if(RankTM.text == "A"&&(GManager.instance.RankNumber2 < 4||GManager.instance.RankNumber2 == null))
            {
                GManager.instance.HiRankStage2 = "A";
                GManager.instance.RankNumber2 = 4;
            }
            if(RankTM.text == "B"&&(GManager.instance.RankNumber2 < 3||GManager.instance.RankNumber2 == null))
            {
                GManager.instance.HiRankStage2 = "B";
                GManager.instance.RankNumber2 = 3;
            }
            if(RankTM.text == "C"&&(GManager.instance.RankNumber2 < 2||GManager.instance.RankNumber2 == null))
            {
                GManager.instance.HiRankStage2 = "C";
                GManager.instance.RankNumber2 = 2;
            }
            if(RankTM.text == "D"&&(GManager.instance.RankNumber2 < 1||GManager.instance.RankNumber2 == null))
            {
                GManager.instance.HiRankStage2 = "D";
                GManager.instance.RankNumber2 = 1;
            }
            if(RankTM.text == "E"&&GManager.instance.RankNumber2 == null)
            {
                GManager.instance.HiRankStage2 = "E";
                GManager.instance.RankNumber2 = 0;
            }
        }
        if(GManager.instance.StageNum == 34)
        {   
            if(RankTM.text == "S"&&(GManager.instance.RankNumber3 < 5||GManager.instance.RankNumber3 == null))
            {
                GManager.instance.HiRankStage3 = "S";
                GManager.instance.RankNumber3 = 5;
            }
            if(RankTM.text == "A"&&(GManager.instance.RankNumber3 < 4||GManager.instance.RankNumber3 == null))
            {
                GManager.instance.HiRankStage3 = "A";
                GManager.instance.RankNumber3 = 4;
            }
            if(RankTM.text == "B"&&(GManager.instance.RankNumber3 < 3||GManager.instance.RankNumber3 == null))
            {
                GManager.instance.HiRankStage3 = "B";
                GManager.instance.RankNumber3 = 3;
            }
            if(RankTM.text == "C"&&(GManager.instance.RankNumber3 < 2||GManager.instance.RankNumber3 == null))
            {
                GManager.instance.HiRankStage3 = "C";
                GManager.instance.RankNumber3 = 2;
            }
            if(RankTM.text == "D"&&(GManager.instance.RankNumber3 < 1||GManager.instance.RankNumber3 == null))
            {
                GManager.instance.HiRankStage3 = "D";
                GManager.instance.RankNumber3 = 1;
            }
            if(RankTM.text == "E"&&GManager.instance.RankNumber3 == null)
            {
                GManager.instance.HiRankStage3 = "E";
                GManager.instance.RankNumber3 = 0;
            }
        }
        if(GManager.instance.StageNum == 44)
        {   
            if(RankTM.text == "S"&&(GManager.instance.RankNumber4 < 5||GManager.instance.RankNumber4 == null))
            {
                GManager.instance.HiRankStage4 = "S";
                GManager.instance.RankNumber4 = 5;
            }
            if(RankTM.text == "A"&&(GManager.instance.RankNumber4 < 4||GManager.instance.RankNumber4 == null))
            {
                GManager.instance.HiRankStage4 = "A";
                GManager.instance.RankNumber4 = 4;
            }
            if(RankTM.text == "B"&&(GManager.instance.RankNumber4 < 3||GManager.instance.RankNumber4 == null))
            {
                GManager.instance.HiRankStage4 = "B";
                GManager.instance.RankNumber4 = 3;
            }
            if(RankTM.text == "C"&&(GManager.instance.RankNumber4 < 2||GManager.instance.RankNumber4 == null))
            {
                GManager.instance.HiRankStage4 = "C";
                GManager.instance.RankNumber4 = 2;
            }
            if(RankTM.text == "D"&&(GManager.instance.RankNumber4 < 1||GManager.instance.RankNumber4 == null))
            {
                GManager.instance.HiRankStage4 = "D";
                GManager.instance.RankNumber4 = 1;
            }
            if(RankTM.text == "E"&&GManager.instance.RankNumber4 == null)
            {
                GManager.instance.HiRankStage4 = "E";
                GManager.instance.RankNumber4 = 0;
            }
        }
    }
    IEnumerator Rankroulette()
    {
        while (ResultDrumroll < 5)
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
        ResultDrumroll += 1;
        }
    }
}
