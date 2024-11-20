using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonManagerOption : MonoBehaviour
{
    [SerializeField] private float horizontal;
    [SerializeField] private float vertical;
// 1フレーム前の十字キーの値
    [SerializeField] private float beforeHorizontal;
    [SerializeField] private float beforeVertical;
    [SerializeField] private Animator cursor = null;
    [SerializeField] private Animator animMaster = null;
    [SerializeField] private Animator animBgm = null;
    [SerializeField] private Animator animSe = null;
    [SerializeField] private Animator animWindow;
    [SerializeField] private TextMeshProUGUI ControlText;
        public enum SELECTED_BUTTON
    {
        MASTER,
        BGM,
        SE,
        CONTROL,
        EXIT
    }
    //シーン開始時に選択されているボタン
    [SerializeField] SELECTED_BUTTON selected = SELECTED_BUTTON.MASTER;
    private void OnEnable() //設定値に合わせたUI見た目の設定
    {
        animMaster.SetInteger("Volume",GManager.instance.VolumeMaster);
        animBgm.SetInteger("Volume",GManager.instance.VolumeBgm);
        animSe.SetInteger("Volume",GManager.instance.VolumeSe);
    }
    private void Start() //設定値に合わせたUI見た目の設定UI見た目の設定
    {
        animMaster.SetInteger("Volume",GManager.instance.VolumeMaster);
        animBgm.SetInteger("Volume",GManager.instance.VolumeBgm);
        animSe.SetInteger("Volume",GManager.instance.VolumeSe);
        if(GManager.instance.isGamepad == true)
        {
            ControlText.text = "GAMEPAD";
        }
        else
        {
            ControlText.text = "KEYBOARD";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!GManager.instance.OptionIsOn)return;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        //各ボタン選択時の分岐処理
        switch(selected)
        {
            case SELECTED_BUTTON.MASTER:
            if(horizontal < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //左が押されたらMASTER音量１段階下げる：前フレームの入力値が0の場合のみ実施
            {
                if(GManager.instance.VolumeMaster != 0)
                {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                GManager.instance.VolumeMaster -= 1;
                SoundManager.Instance.masterVolume -= 0.34f ;
                animMaster.SetInteger("Volume",GManager.instance.VolumeMaster);
                                if(GManager.instance.StageNum == 0)//タイトル画面なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Title);
                }
                if(GManager.instance.StageNum == 1)//ステージセレクト画面なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.LevelSelect);
                }
                else if (GManager.instance.StageNum >= 11 && GManager.instance.StageNum <= 15)//ステージ1なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage);
                }
                else if (GManager.instance.StageNum >= 21 && GManager.instance.StageNum <= 24)//ステージ2なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage2);
                }
                else if (GManager.instance.StageNum >= 31 && GManager.instance.StageNum <= 34)//ステージ3なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage3);
                }
                else if (GManager.instance.StageNum >= 41 && GManager.instance.StageNum <= 44)//ステージ4なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage4);
                }
                }
            }
            else if(horizontal > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //右が押されたらMASTER音量１段階上げる：前フレームの入力値が0の場合のみ実施
            {
                if(GManager.instance.VolumeMaster != 5)
                {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                GManager.instance.VolumeMaster += 1;
                SoundManager.Instance.masterVolume += 0.34f ;
                animMaster.SetInteger("Volume",GManager.instance.VolumeMaster);
                                if(GManager.instance.StageNum == 0)//タイトル画面なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Title);
                }
                if(GManager.instance.StageNum == 1)//ステージセレクト画面なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.LevelSelect);
                }
                else if (GManager.instance.StageNum >= 11 && GManager.instance.StageNum <= 15)//ステージ1なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage);
                }
                else if (GManager.instance.StageNum >= 21 && GManager.instance.StageNum <= 24)//ステージ2なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage2);
                }
                else if (GManager.instance.StageNum >= 31 && GManager.instance.StageNum <= 34)//ステージ3なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage3);
                }
                else if (GManager.instance.StageNum >= 41 && GManager.instance.StageNum <= 44)//ステージ4なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage4);
                }
                }
            }
            if(vertical < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                cursor.SetTrigger("BGM");//カーソルを動かす
                selected = SELECTED_BUTTON.BGM;
            }
            else if(vertical > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                cursor.SetTrigger("EXIT");//カーソルを動かす
                selected = SELECTED_BUTTON.EXIT;
            }
            beforeHorizontal = horizontal; //前フレームの入力値を更新
            beforeVertical = vertical;//前フレームの入力値を更新
            break;
            
            case SELECTED_BUTTON.BGM:
            if(horizontal < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) ////左が押されたらBGM音量１段階下げる：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                if(GManager.instance.VolumeBgm != 0)
                {
                GManager.instance.VolumeBgm -= 1;
                SoundManager.Instance.bgmMasterVolume -= 0.34f ;
                animBgm.SetInteger("Volume",GManager.instance.VolumeBgm);
                if(GManager.instance.StageNum == 0)//タイトル画面なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Title);
                }
                if(GManager.instance.StageNum == 1)//ステージセレクト画面なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.LevelSelect);
                }
                else if (GManager.instance.StageNum >= 11 && GManager.instance.StageNum <= 15)//ステージ1なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage);
                }
                else if (GManager.instance.StageNum >= 21 && GManager.instance.StageNum <= 24)//ステージ2なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage2);
                }
                else if (GManager.instance.StageNum >= 31 && GManager.instance.StageNum <= 34)//ステージ3なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage3);
                }
                else if (GManager.instance.StageNum >= 41 && GManager.instance.StageNum <= 44)//ステージ4なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage4);
                }
                }
            }
            else if(horizontal > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //右が押されたらBGM音量１段階上げる：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                if(GManager.instance.VolumeBgm != 5)
                {
                GManager.instance.VolumeBgm += 1;
                SoundManager.Instance.bgmMasterVolume += 0.34f ;
                animBgm.SetInteger("Volume",GManager.instance.VolumeBgm);
                if(GManager.instance.StageNum == 0)//タイトル画面なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Title);
                }
                if(GManager.instance.StageNum == 1)//ステージセレクト画面なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.LevelSelect);
                }
                else if (GManager.instance.StageNum >= 11 && GManager.instance.StageNum >= 15)//ステージ1なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage);
                }
                else if (GManager.instance.StageNum >= 21 && GManager.instance.StageNum >= 24)//ステージ2なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage2);
                }
                else if (GManager.instance.StageNum >= 31 && GManager.instance.StageNum >= 34)//ステージ3なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage3);
                }
                else if (GManager.instance.StageNum >= 41 && GManager.instance.StageNum >= 44)//ステージ4なら
                {
                    SoundManager.Instance.PauseandResume(BGMSoundData.BGM.Stage4);
                }
                }
            }
            if(vertical < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                cursor.SetTrigger("SE");//カーソルを動かす
                selected = SELECTED_BUTTON.SE;
            }
            else if(vertical > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                cursor.SetTrigger("MASTER");//カーソルを動かす
                selected = SELECTED_BUTTON.MASTER;
            }
            beforeHorizontal = horizontal;//前フレームの入力値を更新
            beforeVertical = vertical;//前フレームの入力値を更新
            break;

            case SELECTED_BUTTON.SE:
            if(horizontal < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //左が押されたらSE音量１段階下げる：前フレームの入力値が0の場合のみ実施
            {
                if(GManager.instance.VolumeSe != 0)
                {
                GManager.instance.VolumeSe -= 1;
                SoundManager.Instance.seMasterVolume -= 0.34f;
                animSe.SetInteger("Volume",GManager.instance.VolumeSe);
                }
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
            }
            else if(horizontal > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //右が押されたらSE音量１段階上げる：前フレームの入力値が0の場合のみ実施
            {
                if(GManager.instance.VolumeSe != 5)
                {
                GManager.instance.VolumeSe += 1;
                SoundManager.Instance.seMasterVolume += 0.34f ;
                animSe.SetInteger("Volume",GManager.instance.VolumeSe);
                }
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
            }
            if(vertical < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                cursor.SetTrigger("CONTROL");//カーソルを動かす
                selected = SELECTED_BUTTON.CONTROL;
            }
            else if(vertical > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                cursor.SetTrigger("BGM");//カーソルを動かす
                selected = SELECTED_BUTTON.BGM;
            }
            beforeHorizontal = horizontal; //前フレームの入力値を更新
            beforeVertical = vertical;//前フレームの入力値を更新
            break;
            
            case SELECTED_BUTTON.CONTROL:
            if(horizontal < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) ////左が押されたら操作設定を変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                if(!GManager.instance.isGamepad)
                {
                    ControlText.text = "GAMEPAD";
                    GManager.instance.isGamepad = true;
                }
                else if(GManager.instance.isGamepad)
                {
                    ControlText.text = "KEYBOARD";
                    GManager.instance.isGamepad = false;
                }
            }
            else if(horizontal > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //右が押されたら操作設定を変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                if(!GManager.instance.isGamepad)
                {
                    ControlText.text = "GAMEPAD";
                    GManager.instance.isGamepad = true;
                }
                else if(GManager.instance.isGamepad)
                {
                    ControlText.text = "KEYBOARD";
                    GManager.instance.isGamepad = false;
                }
            }
            if(vertical < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                cursor.SetTrigger("EXIT");//カーソルを動かす
                selected = SELECTED_BUTTON.EXIT;
            }
            else if(vertical > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                cursor.SetTrigger("SE");//カーソルを動かす
                selected = SELECTED_BUTTON.SE;
            }
            if(Input.GetButtonDown("Jump")) //決定キーが押されたらアニメーショントリガー"IsPushed"発動、IsPushed変数を真に
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                cursor.SetTrigger("SE");//カーソルを動かす
                selected = SELECTED_BUTTON.SE;
            }
            beforeHorizontal = horizontal;//前フレームの入力値を更新
            beforeVertical = vertical;//前フレームの入力値を更新
            break;

            case SELECTED_BUTTON.EXIT:
            if(vertical < 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //下が押されたら選択中のボタンを変更：連続で入力されるのを防ぐため前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                cursor.SetTrigger("MASTER");//カーソルを動かす
                selected = SELECTED_BUTTON.MASTER;
            }
            else if(vertical > 0f && beforeHorizontal == 0.0f && beforeVertical == 0.0f) //上が押されたら選択中のボタンを変更：前フレームの入力値が0の場合のみ実施
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Cursor);
                cursor.SetTrigger("CONTROL");//カーソルを動かす
                selected = SELECTED_BUTTON.CONTROL;
            }
            if(Input.GetButtonDown("Jump")) //決定キーが押されたらアニメーショントリガー"IsPushed"発動、IsPushed変数を真に
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.MenuOut);
                cursor.SetTrigger("EXITPushed");//カーソルを決定
                animWindow.SetTrigger("OptionExit");//終了するアニメーション入れる（アニメーションイベントでOptionEnd呼ぶ）
                selected = SELECTED_BUTTON.MASTER;
            }
            beforeHorizontal = horizontal;//前フレームの入力値を更新
            beforeVertical = vertical;//前フレームの入力値を更新
            break;
        }
    }
}
