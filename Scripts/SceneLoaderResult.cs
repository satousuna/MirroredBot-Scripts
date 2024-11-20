using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderResult : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionwaitTime = 0f;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] GameObject ButtonManager;
    private ButtonManagerResult ButtonManagerResult;
    private void Start() 
    {
        ButtonManagerResult = ButtonManager.GetComponent<ButtonManagerResult>();
        ButtonManager.SetActive(false);
    }
    void Update()
    {
        if(ButtonManagerResult.GetButton0())
        {
            LoadRestart();
        }
        if(ButtonManagerResult.GetButton1())
        {
            LoadLevelSelect();
        }
    }

    public void LoadRestart()
    {
        if(GManager.instance.StageNum == 15)
        {
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Stage);
            StartCoroutine(LoadRestart(3));
            GManager.instance.StageNum = 11;//ステージナンバー初期化
            GManager.instance.ClearTime = 0.0f;
            GManager.instance.TimerOn = true;
            GManager.instance.DeathCount = 0;
            GManager.instance.GemCount = 0;
        }
        else if(GManager.instance.StageNum == 24)
        {
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Stage2);
            StartCoroutine(LoadRestart(8));
            GManager.instance.StageNum = 21;//ステージナンバー初期化
            GManager.instance.ClearTime = 0.0f;
            GManager.instance.TimerOn = true;
            GManager.instance.DeathCount = 0;
            GManager.instance.GemCount = 0;
        }
        else if(GManager.instance.StageNum == 34)
        {
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Stage3);
        StartCoroutine(LoadRestart(12));
        GManager.instance.StageNum = 31;//ステージナンバー初期化
        GManager.instance.ClearTime = 0.0f;
        GManager.instance.TimerOn = true;
        GManager.instance.DeathCount = 0;
        GManager.instance.GemCount = 0;
        }
        else if(GManager.instance.StageNum == 44)
        {   
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Stage4);
        StartCoroutine(LoadRestart(16));
        GManager.instance.StageNum = 41;//ステージナンバー初期化
        GManager.instance.ClearTime = 0.0f;
        GManager.instance.TimerOn = true;
        GManager.instance.DeathCount = 0;
        GManager.instance.GemCount = 0;
        }
    }

    IEnumerator LoadRestart(int SceneIndex)
    {
        yield return new WaitForSeconds(transitionwaitTime);

        transition.SetTrigger("TransitionStart");

        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(SceneIndex);
    }

    public void LoadLevelSelect()
    {
        StartCoroutine(LoadLevelSelect(1));
    }

    IEnumerator LoadLevelSelect(int SceneIndex)
    {
        yield return new WaitForSeconds(transitionwaitTime);

        transition.SetTrigger("TransitionStart");

        yield return new WaitForSeconds(transitionTime);
        if(GManager.instance.Allclear = false&&GManager.instance.HiRankStage1 != ""&&GManager.instance.HiRankStage2 != "" &&GManager.instance.HiRankStage3 != ""&&GManager.instance.HiRankStage4 != "")
        {
        SceneManager.LoadScene(20);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

}
