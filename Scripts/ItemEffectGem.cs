using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class ItemEffectGem : MonoBehaviour
{
    private string PlayerTag = "Player";
    [SerializeField] GameObject Player;
    private PlayerMovement PlayerMovement;
    private bool GemDisappear;
    private Animator anim = null;
    public ParticleSystem particles;
    private void Start() 
    {
        anim = GetComponent<Animator>();
        PlayerMovement = Player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == PlayerTag&&GemDisappear == false)
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.ItemGet);
            GManager.instance.GemCount += 1;
            GemDisappear = true;
            particles.Play();
            anim.SetTrigger("ItemGet");
        }
    }

    public bool GetGem()
    {
        return GemDisappear;//アイテムの獲得状況を取得
    }

    void Update()
    {
        if(PlayerMovement.GetDeath()&&GemDisappear == true)//ジェムを獲得した状態で死亡したら
        {
            GManager.instance.GemCount -= 1;//死亡カウント加算
            GemDisappear = false;
        }
    }

}
