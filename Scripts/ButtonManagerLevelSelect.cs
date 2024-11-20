using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManagerLevelSelect : MonoBehaviour
{
    [SerializeField] private float horizontal;
    [SerializeField] private float vertical;
// 1フレーム前の十字キーの値
    [SerializeField] private float beforeHorizontal;
    [SerializeField] private float beforeVertical;
//各ボタンのアニメーター
    [SerializeField] private Animator Button0;
    [SerializeField] private Animator Button1;
    [SerializeField] private Animator Button2;
    [SerializeField] private Animator Button3;
    [SerializeField] private Animator Button4;
    [SerializeField] private Animator Button5;
    [SerializeField] private Animator Option;
    [SerializeField] GameObject OptionWindow;

//各ボタンの変数
    public enum SELECTED_BUTTON
    {
        STAGE1,
        STAGE2,
        STAGE3,
        STAGE4,
        TITLE,
        OPTION
    }
    //各ボタンが押されたときに受け渡す変数
    public bool Button0IsPushed;
    public bool Button1IsPushed;
    public bool Button2IsPushed;
    public bool Button3IsPushed;
    public bool Button4IsPushed;
    public bool Button5IsPushed;
//シーン開始時に選択されているボタン
    [SerializeField] SELECTED_BUTTON selected = SELECTED_BUTTON.STAGE1;
//シーン開始時に選択されているボタンのアニメーションをセット
    private void Start() 
    {
        Button0.SetBool("IsSelected", true);
    }
    void Update()
    {
        if(GManager.instance.OptionIsOn)return;//Option開いているときは動かさない
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        //各ボタン選択時の分岐処理
        switch(selected)
        {
            case SELECTED_BUTTON.STAGE1:
            if(horizontal < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //左が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button0.SetBool("IsSelected", false);
                Button0.SetTrigger("Reset");
                Button3.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.STAGE4;
            }
            else if(horizontal > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //右が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button0.SetBool("IsSelected", false);
                Button0.SetTrigger("Reset");
                Button1.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.STAGE2;
            }
            if(vertical < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button0.SetBool("IsSelected", false);
                Button0.SetTrigger("Reset");
                Button4.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.TITLE;
            }
            else if(vertical > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button0.SetBool("IsSelected", false);
                Button0.SetTrigger("Reset");
                Button4.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.TITLE;
            }
            if(Input.GetButtonDown("Jump")) ////決定キーが押されたらアニメーショントリガー"IsPushed"発動、IsPushed変数を真に
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Select);
                Button0.SetTrigger("IsPushed");
                Button0IsPushed = true;//他オブジェクトに渡す変数
            }
            beforeHorizontal = horizontal; //前フレームの入力値を更新
            beforeVertical = vertical;//前フレームの入力値を更新
            break;
            
            case SELECTED_BUTTON.STAGE2:
            if(horizontal < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) ////左が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button1.SetBool("IsSelected", false);
                Button1.SetTrigger("Reset");
                Button0.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.STAGE1;
            }
            else if(horizontal > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //右が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button1.SetBool("IsSelected", false);
                Button1.SetTrigger("Reset");
                Button2.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.STAGE3;
            }
            if(vertical < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button1.SetBool("IsSelected", false);
                Button1.SetTrigger("Reset");
                Button4.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.TITLE;
            }
            else if(vertical > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button1.SetBool("IsSelected", false);
                Button1.SetTrigger("Reset");
                Button4.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.TITLE;
            }
            if(Input.GetButtonDown("Jump")) //決定キーが押されたらアニメーショントリガー"IsPushed"発動、IsPushed変数を真に
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Select);
                Button1.SetTrigger("IsPushed");
                Button1IsPushed = true;//他オブジェクトに渡す変数
            }
            beforeHorizontal = horizontal;//前フレームの入力値を更新
            beforeVertical = vertical;//前フレームの入力値を更新
            break;

            case SELECTED_BUTTON.STAGE3:
            if(horizontal < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //左が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button2.SetBool("IsSelected", false);
                Button2.SetTrigger("Reset");
                Button1.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.STAGE2;
            }
            else if(horizontal > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //右が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button2.SetBool("IsSelected", false);
                Button2.SetTrigger("Reset");
                Button3.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.STAGE4;
            }
            if(vertical < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button2.SetBool("IsSelected", false);
                Button2.SetTrigger("Reset");
                Button5.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.OPTION;
            }
            else if(vertical > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button2.SetBool("IsSelected", false);
                Button2.SetTrigger("Reset");
                Button5.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.OPTION;
            }
            if(Input.GetButtonDown("Jump")) ////決定キーが押されたらアニメーショントリガー"IsPushed"発動、IsPushed変数を真に
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Select);
                Button2.SetTrigger("IsPushed");
                Button2IsPushed = true;//他オブジェクトに渡す変数
            }
            beforeHorizontal = horizontal; //前フレームの入力値を更新
            beforeVertical = vertical;//前フレームの入力値を更新
            break;
            
            case SELECTED_BUTTON.STAGE4:
            if(horizontal < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) ////左が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button3.SetBool("IsSelected", false);
                Button3.SetTrigger("Reset");
                Button2.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.STAGE3;
            }
            else if(horizontal > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //右が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button3.SetBool("IsSelected", false);
                Button3.SetTrigger("Reset");
                Button0.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.STAGE1;
            }
            if(vertical < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button3.SetBool("IsSelected", false);
                Button3.SetTrigger("Reset");
                Button5.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.OPTION;
            }
            else if(vertical > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button3.SetBool("IsSelected", false);
                Button3.SetTrigger("Reset");
                Button5.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.OPTION;
            }
            if(Input.GetButtonDown("Jump")) //決定キーが押されたらアニメーショントリガー"IsPushed"発動、IsPushed変数を真に
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Select);
                Button3.SetTrigger("IsPushed");
                Button3IsPushed = true;//他オブジェクトに渡す変数
            }
            beforeHorizontal = horizontal;//前フレームの入力値を更新
            beforeVertical = vertical;//前フレームの入力値を更新
            break;

            case SELECTED_BUTTON.TITLE:
            if(horizontal < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) ////左が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button4.SetBool("IsSelected", false);
                Button4.SetTrigger("Reset");
                Button5.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.OPTION;
            }
            else if(horizontal > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //右が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button4.SetBool("IsSelected", false);
                Button4.SetTrigger("Reset");
                Button5.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.OPTION;
            }
            if(vertical < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button4.SetBool("IsSelected", false);
                Button4.SetTrigger("Reset");
                Button0.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.STAGE1;
            }
            else if(vertical > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button4.SetBool("IsSelected", false);
                Button4.SetTrigger("Reset");
                Button0.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.STAGE1;
            }
            if(Input.GetButtonDown("Jump")) //決定キーが押されたらアニメーショントリガー"IsPushed"発動、IsPushed変数を真に
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Select);
                Button4.SetTrigger("IsPushed");
                Button4IsPushed = true;//他オブジェクトに渡す変数
            }
            beforeHorizontal = horizontal;//前フレームの入力値を更新
            beforeVertical = vertical;//前フレームの入力値を更新
            break;

            case SELECTED_BUTTON.OPTION:
            if(horizontal < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) ////左が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button5.SetBool("IsSelected", false);
                Button5.SetTrigger("Reset");
                Button4.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.TITLE;
            }
            else if(horizontal > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //右が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button5.SetBool("IsSelected", false);
                Button5.SetTrigger("Reset");
                Button4.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.TITLE;
            }
            if(vertical < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button5.SetBool("IsSelected", false);
                Button5.SetTrigger("Reset");
                Button2.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.STAGE3;
            }
            else if(vertical > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                Button5.SetBool("IsSelected", false);
                Button5.SetTrigger("Reset");
                Button2.SetBool("IsSelected", true);
                selected = SELECTED_BUTTON.STAGE3;
            }
            if(Input.GetButtonDown("Jump")) //決定キーが押されたらアニメーショントリガー"IsPushed"発動、IsPushed変数を真に
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.MenuIn);
                Button2.SetTrigger("IsPushed");
                OptionWindow.SetActive(true);
                Option.SetTrigger("OptionEnter");
                GManager.instance.OptionIsOn = true;
            }
            beforeHorizontal = horizontal;//前フレームの入力値を更新
            beforeVertical = vertical;//前フレームの入力値を更新
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
    public bool GetButton2()
    {
        return Button2IsPushed;
    }
    public bool GetButton3()
    {
        return Button3IsPushed;
    }
    public bool GetButton4()
    {
        return Button4IsPushed;
    }
}
