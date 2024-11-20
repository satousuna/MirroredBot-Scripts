using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManagerResult : MonoBehaviour
{
    [SerializeField] private float horizontal;
// 1フレーム前の十字キーの値
    [SerializeField] private float beforeHorizontal;
//各ボタンのアニメーター
    [SerializeField] private Animator Button0;
    [SerializeField] private Animator Button1;
//各ボタンの変数
    public enum SELECTED_BUTTON
    {
        RESTART,
        EXIT,
    }
    //各ボタンが押されたときに受け渡す変数
    public bool Button0IsPushed;
    public bool Button1IsPushed;
//シーン開始時に選択されているボタン
    [SerializeField] SELECTED_BUTTON selected = SELECTED_BUTTON.RESTART;
//シーン開始時に選択されているボタンのアニメーションをセット
    private void Start() 
    {
        Button0.SetBool("IsSelected", true);
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        //各ボタン選択時の分岐処理
        switch(selected)
        {
            case SELECTED_BUTTON.RESTART:
            if(horizontal < 0f && beforeHorizontal == 0.0f) //左が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button0.SetBool("IsSelected", false);
                Button0.SetTrigger("Reset");
                Button1.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.EXIT;
            }
            else if(horizontal > 0f && beforeHorizontal == 0.0f) //右が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button0.SetBool("IsSelected", false);
                Button0.SetTrigger("Reset");
                Button1.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.EXIT;
            }
            if(Input.GetButtonDown("Jump")) ////決定キーが押されたらアニメーショントリガー"IsPushed"発動、IsPushed変数を真に
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Select);
                Button0.SetTrigger("IsPushed");
                Button0IsPushed = true;//他オブジェクトに渡す変数
            }
            beforeHorizontal = horizontal; //前フレームの入力値を更新
            break;
            
            case SELECTED_BUTTON.EXIT:
            if(horizontal < 0f && beforeHorizontal == 0.0f) ////左が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button1.SetBool("IsSelected", false);
                Button1.SetTrigger("Reset");
                Button0.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.RESTART;
            }
            else if(horizontal > 0f && beforeHorizontal == 0.0f) //右が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button1.SetBool("IsSelected", false);
                Button1.SetTrigger("Reset");
                Button0.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.RESTART;
            }
            if(Input.GetButtonDown("Jump")) //決定キーが押されたらアニメーショントリガー"IsPushed"発動、IsPushed変数を真に
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Select);
                Button1.SetTrigger("IsPushed");
                Button1IsPushed = true;//他オブジェクトに渡す変数
            }
            beforeHorizontal = horizontal;//前フレームの入力値を更新
            break;
        }
    }
    public bool GetButton0()
    {
        return Button0IsPushed;
    }

    public bool GetButton1()
    {
        return Button1IsPushed;
    }

}