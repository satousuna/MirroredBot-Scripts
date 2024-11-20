using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private Animator Retry;
    [SerializeField] private float transitionwaitTime = 0f;
    [SerializeField] private float transitionTime = 1f;

    [SerializeField] GameObject Player;
    [SerializeField] GameObject GoalDoor;
    private PlayerMovement PlayerMovement;
    private DoorOpen DoorOpen;
    private bool StageNumfixed;
    private void Start() 
    {
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        DoorOpen = GoalDoor.GetComponent<DoorOpen>();
    }
    void Update() 
    {    
        if(DoorOpen.GetDoor())
        {
            if(!StageNumfixed)
            {
                GManager.instance.StageNum += 1;
                StageNumfixed = true;
            }
            LoadNextScene();
        }
        else if(PlayerMovement.GetRetry())
        {
            LoadRetry();
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

        public void LoadRetry()
    {
        StartCoroutine(LoadRetry(SceneManager.GetActiveScene().buildIndex));
    }
    public void LoadExit()
    {
        StartCoroutine(LoadExit(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadNextScene(int SceneIndex)
    {
        yield return new WaitForSeconds(transitionwaitTime);
        transition.SetTrigger("TransitionStart");

        yield return new WaitForSeconds(transitionTime);
        

        SceneManager.LoadScene(SceneIndex);
    }

        IEnumerator LoadRetry(int SceneIndex)
    {
        yield return new WaitForSeconds(transitionwaitTime);

        Retry.SetTrigger("RetryStart");

        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(SceneIndex);
    }

    IEnumerator LoadExit(int SceneIndex)
    {
        yield return new WaitForSeconds(transitionwaitTime);

        transition.SetTrigger("TransitionStart");

        yield return new WaitForSeconds(transitionTime);
        
        GManager.instance.StageNum = 10;
        SceneManager.LoadScene(1);
    }

}
