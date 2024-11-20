using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyOpen : MonoBehaviour
{
    private Animator anim = null;
    void Start()
    {
        anim = GetComponent<Animator>();
        if (GManager.instance.StageNum == 15)//ステージごとにキー目標数を設定&KeyOpenを偽に
        {
        GManager.instance.KeyOpen = false;
        GManager.instance.KeyGoal = 5;
        }
        if (GManager.instance.StageNum == 22)//ステージごとにキー目標数を設定&KeyOpenを偽に
        {
        GManager.instance.KeyOpen = false;
        GManager.instance.KeyGoal = 5;
        }
        if (GManager.instance.StageNum == 32)//ステージごとにキー目標数を設定&KeyOpenを偽に
        {
        GManager.instance.KeyOpen = false;
        GManager.instance.KeyGoal = 6;
        }
        if (GManager.instance.StageNum == 41)//ステージごとにキー目標数を設定&KeyOpenを偽に
        {
        GManager.instance.KeyOpen = false;
        GManager.instance.KeyGoal = 4;
        }
        if (GManager.instance.StageNum == 42)//ステージごとにキー目標数を設定&KeyOpenを偽に
        {
        GManager.instance.KeyOpen = false;
        GManager.instance.KeyGoal = 4;
        }
        if (GManager.instance.StageNum == 43)//ステージごとにキー目標数を設定&KeyOpenを偽に
        {
        GManager.instance.KeyOpen = false;
        GManager.instance.KeyGoal = 6;
        }
                if (GManager.instance.StageNum == 44)//ステージごとにキー目標数を設定&KeyOpenを偽に
        {
        GManager.instance.KeyOpen = false;
        GManager.instance.KeyGoal = 5;
        }
    }
    void Update()
    {
        if(GManager.instance.KeyCount == GManager.instance.KeyGoal)//キー目標数が取得数と等しいとき
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.KeyOpen);
            anim.SetTrigger("KeyOpen");
            GManager.instance.KeyCount = 0;//キー取得数リセット
            GManager.instance.KeyOpen = true;
        }
    }
}
