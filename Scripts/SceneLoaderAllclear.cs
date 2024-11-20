using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderAllclear : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionwaitTime = 0f;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private GameObject Text1;
    [SerializeField] private GameObject Text2;
    [SerializeField] private GameObject Text3;
    private bool AllClearfinish;
    private void Start() 
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Ending);
        StartCoroutine ("Allclear");
    }

    void Update()
    {
        if(Input.GetButtonUp("Jump")&&AllClearfinish)
        {
            LoadSelectedLevel(0);
        }
    }
        IEnumerator Allclear()
    {
        GManager.instance.Allclear = true;
        yield return new WaitForSeconds(1.5f);
        Text1.SetActive(true);//RESULT表示
        SoundManager.Instance.PlaySE(SESoundData.SE.ResultHead);//リザルト効果音
        yield return new WaitForSeconds(1.5f);
        Text2.SetActive(true);//GEM表示
        SoundManager.Instance.PlaySE(SESoundData.SE.ResultHead);//リザルト効果音
        yield return new WaitForSeconds(3f);
        Text3.SetActive(true);//GEM表示
        AllClearfinish = true;
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
        SceneManager.LoadScene(SceneIndex);
    }
}
