using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderTitle : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionwaitTime = 0f;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] GameObject ButtonManager;
    private ButtonManagerTitle ButtonManagerTitle;
    private void Start() 
    {
        GManager.instance.StageNum = 0;
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);//タイトルBGM再生
        ButtonManagerTitle = ButtonManager.GetComponent<ButtonManagerTitle>();
    }
    void Update()
    {
        if(ButtonManagerTitle.GetButton0())
        {
            LoadNextScene();//ステージセレクト画面遷移
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1));//画面遷移コルーチン開始
    }

    IEnumerator LoadNextScene(int SceneIndex)
    {
        yield return new WaitForSeconds(transitionwaitTime);

        transition.SetTrigger("TransitionStart");

        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(SceneIndex);
    }

}
