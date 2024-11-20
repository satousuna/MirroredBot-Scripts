using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    public int StageNum;
    public int GemCount;
    public int KeyCount;
    public int KeyGoal;
    public int DeathCount;
    public int VolumeMaster;
    public int VolumeBgm ;
    public int VolumeSe;
    public bool KeyOpen = true;
    public bool IsPaused;
    public bool TimerOn;
    public bool OptionIsOn;
    public bool isGamepad;
    public bool Allclear;
    public float ClearTime;
    public string HiRankStage1;
    public int? RankNumber1;
    public string HiRankStage2;
    public int? RankNumber2;
    public string HiRankStage3;
    public int? RankNumber3;
    public string HiRankStage4;
    public int? RankNumber4;

    void Awake()
    {
        VolumeMaster = 3;
        VolumeBgm = 3;
        VolumeSe = 3;
        if(instance == null)
         {
             instance = this;
             DontDestroyOnLoad(this.gameObject); 
         }
         else
         {
             Destroy(this.gameObject);
         }
    }
    void Update()
    {
        if(TimerOn == true)
        {
            ClearTime += Time.deltaTime;
        }
    }
}
