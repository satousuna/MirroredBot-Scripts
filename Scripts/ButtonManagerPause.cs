using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonManagerPause : MonoBehaviour
{
    [SerializeField] private float vertical;
// 1フレーム前の十字キーの値
    [SerializeField] private float beforeVertical;
//各ボタンのアニメーター
    [SerializeField] private Animator Button0;
    [SerializeField] private Animator Button1;
    [SerializeField] private Animator Button2;
    [SerializeField] private Animator Button3;
    [SerializeField] private Animator Option;
    [SerializeField] private GameObject PauseCanvas;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject SceneLoader;
    [SerializeField] GameObject OptionWindow;
    private PlayerMovement PlayerMovement;
    private SceneLoader sceneLoader;
    
//各ボタンの変数
    public enum SELECTED_BUTTON
    {
        RESUME,
        RETRY,
        OPTION,
        EXIT,
    }
    //各ボタンが押されたときに受け渡す変数
    public bool Button0IsPushed;
    public bool Button1IsPushed;
    public bool Button2IsPushed;
    public bool Button3IsPushed;
//シーン開始時に選択されているボタン
    [SerializeField] SELECTED_BUTTON selected = SELECTED_BUTTON.RESUME;
//シーン開始時に選択されているボタンのアニメーションをセット
    private void Start() 
    {
        Button0.SetBool("IsSelected", true);
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        sceneLoader = SceneLoader.GetComponent<SceneLoader>();
    }
    private void OnEnable() 
    {
        Button0.SetTrigger("Reset");//アニメーションを初期化
        Button1.SetTrigger("Reset");
        Button2.SetTrigger("Reset");
        Button3.SetTrigger("Reset");
        Button0.SetBool("IsSelected", true);
        selected = SELECTED_BUTTON.RESUME;//ボタン選択状況を初期化
        GManager.instance.IsPaused = true;
    }
    void Update()
    {
        if(GManager.instance.OptionIsOn)return;//Option開いているときは動かさない
        vertical = Input.GetAxisRaw("Vertical");
        //各ボタン選択時の分岐処理
        switch(selected)
        {
            case SELECTED_BUTTON.RESUME:
            if(vertical < 0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button0.SetBool("IsSelected", false);
                Button0.SetTrigger("Reset");
                Button1.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.RETRY;
            }
            else if(vertical > 0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button0.SetBool("IsSelected", false);
                Button0.SetTrigger("Reset");
                Button3.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.EXIT;
            }
            if(Input.GetButtonDown("Jump")) ////決定キーが押されたらポーズメニューの状態を初期化して非アクティブ化
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Select);
                OptionClose();
            }
            beforeVertical = vertical;//前フレームの入力値を更新
            break;
            
            case SELECTED_BUTTON.RETRY:
            if(vertical < 0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button1.SetBool("IsSelected", false);
                Button1.SetTrigger("Reset");
                Button2.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.OPTION;
            }
            else if(vertical > 0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button1.SetBool("IsSelected", false);
                Button1.SetTrigger("Reset");
                Button0.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.RESUME;
            }
            if(Input.GetButtonDown("Jump")) //決定キーが押されたらアニメーショントリガー"IsPushed"発動、IsPushed変数を真に
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Select);
                OptionRetry();
            }
            beforeVertical = vertical;//前フレームの入力値を更新
            break;

            case SELECTED_BUTTON.OPTION:
            if(vertical < 0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button2.SetBool("IsSelected", false);
                Button2.SetTrigger("Reset");
                Button3.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.EXIT;
            }
            else if(vertical > 0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button2.SetBool("IsSelected", false);
                Button2.SetTrigger("Reset");
                Button1.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.RETRY;
            }
            if(Input.GetButtonDown("Jump")) ////決定キーが押されたらアニメーショントリガー"IsPushed"発動、IsPushed変数を真に
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.MenuIn);
                Button2.SetTrigger("IsPushed");
                OptionWindow.SetActive(true);
                Option.SetTrigger("OptionEnter");
                GManager.instance.OptionIsOn = true;
                Button2IsPushed = true;//他オブジェクトに渡す変数
            }
            beforeVertical = vertical;//前フレームの入力値を更新
            break;
            
            case SELECTED_BUTTON.EXIT:
            if(vertical < 0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button3.SetBool("IsSelected", false);
                Button3.SetTrigger("Reset");
                Button0.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.RESUME;
            }
            else if(vertical > 0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button3.SetBool("IsSelected", false);
                Button3.SetTrigger("Reset");
                Button2.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.OPTION;
            }
            if(Input.GetButtonDown("Jump")) //決定キーが押されたらアニメーショントリガー"IsPushed"発動、IsPushed変数を真に
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Select);
                Button3.SetTrigger("IsPushed");
                OptionExit();
            }
            beforeVertical = vertical;//前フレームの入力値を更新
            break;
        }
        if (Input.GetKeyDown(KeyCode.Escape))//ESCキーでポーズ画面閉じる
        {
        OptionClose();    
        }
    }
    private void OptionClose()
    {
        PlayerMovement.PlayerStart();//PlayerのisStopped変数を偽にする
        GManager.instance.IsPaused = false;//Gmanagerのポーズ変数を偽にする
        Time.timeScale = 1f;
        PauseCanvas.SetActive(false);//ポーズ画面を非アクティブ化
    }

    private void OptionRetry()
    {
        PlayerMovement.PlayerStart();//PlayerのisStopped変数を偽にする
        GManager.instance.IsPaused = false;//Gmanagerのポーズ変数を偽にする
        Time.timeScale = 1f;
        PlayerMovement.PlayerDeath();
        PauseCanvas.SetActive(false);//ポーズ画面を非アクティブ化
    }
    private void OptionExit()
    {
        PlayerMovement.PlayerStart();//PlayerのisStopped変数を偽にする
        GManager.instance.IsPaused = false;//Gmanagerのポーズ変数を偽にする
        Time.timeScale = 1f;
        sceneLoader.LoadExit();
        PauseCanvas.SetActive(false);//ポーズ画面を非アクティブ化
    }
}
