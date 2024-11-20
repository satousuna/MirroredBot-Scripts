using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class ItemEffectKey : MonoBehaviour
{
    private string PlayerTag = "Player";
    [SerializeField] GameObject Player;
    private PlayerMovement PlayerMovement;
    private bool KeyDisappear;
    private Animator anim = null;
    public ParticleSystem particles;
    private void Start() 
    {
        anim = GetComponent<Animator>();
        PlayerMovement = Player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {

        if(other.tag == PlayerTag&&KeyDisappear == false)
        {
            GManager.instance.KeyCount += 1;
            KeyDisappear = true;
            particles.Play();
            SoundManager.Instance.PlaySE(SESoundData.SE.KeyGet);
            anim.SetTrigger("ItemGet");
        }
    }

    public bool GetKey()
    {
        return KeyDisappear;//アイテムの獲得状況を取得
    }

    void Update()
    {
        if(PlayerMovement.GetDeath()&&KeyDisappear == true)//ジェムを獲得した状態で死亡していたら
        {
            GManager.instance.KeyCount = 0;//キー数リセット
            KeyDisappear = false;
        }
    }

}
