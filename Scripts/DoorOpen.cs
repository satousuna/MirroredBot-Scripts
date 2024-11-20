using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private float vertical;
    private string PlayerTag = "Player";
    [SerializeField]private bool isOpenable;
    private bool DoorIsOpen;
    [SerializeField] private Animator Arrow;
    private Animator anim = null;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

//プレイヤーが判定に入ったときisOpenableを真に、矢印出す
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == PlayerTag)
        {
            isOpenable = true;
            Arrow.SetBool("IsAppearing", true);
        }
    }
//プレイヤーが判定を出たときisOpenableを偽に、矢印消す
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == PlayerTag)
        {
            isOpenable = false;
            Arrow.SetBool("IsAppearing", false);
        }
    }

    void Update()
    {
        if(!isOpenable)return;//isOpenableが真でないときはドアを開ける処理を行わない
        
        //縦方向の入力受け取り
        vertical = Input.GetAxisRaw("Vertical");
        if(vertical > 0f&&GManager.instance.KeyOpen)
        {
            anim.SetTrigger("IsOpen");//開扉アニメーション
            DoorIsOpen = true;//開扉フラグを真に
        }    
    }

    public bool GetDoor()//シーンマネージャに開扉フラグを渡す
    {
        return DoorIsOpen;
    }

    private void DoorSE()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.DoorOpen);
    }

}
