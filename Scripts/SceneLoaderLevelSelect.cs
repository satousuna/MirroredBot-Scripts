using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderLevelSelect : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionwaitTime = 0f;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] GameObject ButtonManager;
    private ButtonManagerLevelSelect buttonManager;
    private void Start() 
    {
        GManager.instance.StageNum = 1;
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.LevelSelect);
        buttonManager = ButtonManager.GetComponent<ButtonManagerLevelSelect>();
    }
    void Update()
    {
        if(buttonManager.GetButton0())
        {
            GManager.instance.StageNum = 11;//ステージナンバーに11を代入
            LoadSelectedLevel(3);
            GManager.instance.ClearTime = 0.0f;
            GManager.instance.TimerOn = true;
            GManager.instance.DeathCount = 0;
            GManager.instance.GemCount = 0;
        }
        if(buttonManager.GetButton1())
        {
            GManager.instance.StageNum = 21;//ステージナンバーに21を代入
            LoadSelectedLevel(8);
            GManager.instance.ClearTime = 0.0f;
            GManager.instance.TimerOn = true;
            GManager.instance.DeathCount = 0;
            GManager.instance.GemCount = 0;
        }
        if(buttonManager.GetButton2())
        {
            GManager.instance.StageNum = 31;//ステージナンバーに31を代入
            LoadSelectedLevel(12);
            GManager.instance.ClearTime = 0.0f;
            GManager.instance.TimerOn = true;
            GManager.instance.DeathCount = 0;
            GManager.instance.GemCount = 0;
        }
        if(buttonManager.GetButton3())
        {
            GManager.instance.StageNum = 41;//ステージナンバーに41を代入
            LoadSelectedLevel(16);
            GManager.instance.ClearTime = 0.0f;
            GManager.instance.TimerOn = true;
            GManager.instance.DeathCount = 0;
            GManager.instance.GemCount = 0;
        }
        if(buttonManager.GetButton4())
        {
            LoadTitle(0);
        }
    }

    public void LoadSelectedLevel(int SceneIndex)
    {
        StartCoroutine(LoadNextScene(SceneIndex));
    }

    IEnumerator LoadNextScene(int SceneIndex)
    {
        yield return new WaitForSeconds(transitionwaitTime);
        transition.SetTrigger("TransitionStart");

        yield return new WaitForSeconds(transitionTime);
        if(GManager.instance.StageNum == 11)
        {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Stage);
        }
        else if(GManager.instance.StageNum == 21)
        {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Stage2);
        }
        else if(GManager.instance.StageNum == 31)
        {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Stage3);
        }
        else if(GManager.instance.StageNum == 41)
        {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Stage4);
        }
        SceneManager.LoadScene(SceneIndex);
    }
        public void LoadTitle(int SceneIndex)
    {
        StartCoroutine(LoadNextScene(SceneIndex));
    }
        IEnumerator LoadTitleScene(int SceneIndex)
    {
        yield return new WaitForSeconds(transitionwaitTime);
        transition.SetTrigger("TransitionStart");

        yield return new WaitForSeconds(transitionTime);
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
        SceneManager.LoadScene(SceneIndex);
    }

}
