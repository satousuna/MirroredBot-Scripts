using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderClearLevel : MonoBehaviour//ClearLevelとはシーン遷移時の演出が異なる
{
    [SerializeField] private Animator transition;
    [SerializeField] private Animator Retry;
    [SerializeField] private Animator ClearLevelText;
    private Animator animClearText = null;
    [SerializeField] private float transitionwaitTime = 0f;
    [SerializeField] private float transitionTime = 1f;

    [SerializeField] GameObject Player;
    [SerializeField] GameObject GoalDoor;
    [SerializeField] GameObject ClearText;
    private PlayerMovement PlayerMovement;
    private DoorOpen DoorOpen;
    private void Start() 
    {
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        DoorOpen = GoalDoor.GetComponent<DoorOpen>();
        animClearText = ClearLevelText.GetComponent<Animator>();
    }
    void Update()
    {
        if(DoorOpen.GetDoor())
        {
            LoadNextScene();
        }
        else if(PlayerMovement.GetRetry())
        {
            LoadRetry();
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadNextScene(2));
    }

        public void LoadRetry()
    {
        StartCoroutine(LoadRetry(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadNextScene(int SceneIndex)
    {
        GManager.instance.IsPaused = true;
        GManager.instance.TimerOn = false;
        PlayerMovement.PlayerStop();
        yield return new WaitForSeconds(1f);

        ClearLevelText.SetTrigger("Clear");

        yield return new WaitForSeconds(2f);


        transition.SetTrigger("TransitionStart");

        yield return new WaitForSeconds(transitionTime);
        
        GManager.instance.IsPaused = false;
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Result);
        SceneManager.LoadScene(SceneIndex);
    }

        IEnumerator LoadRetry(int SceneIndex)
    {
        yield return new WaitForSeconds(transitionwaitTime);

        Retry.SetTrigger("RetryStart");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneIndex);
    }

}
