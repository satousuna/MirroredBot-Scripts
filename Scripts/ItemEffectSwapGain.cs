using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectSwapGain : MonoBehaviour
{
    private string PlayerTag = "Player";
    [SerializeField] GameObject Player;
    private PlayerMovement PlayerMovement;
    private bool isActive = true;
    private Animator anim = null;
    public ParticleSystem particles;

    private void Start() 
    {
        anim = GetComponent<Animator>();
        PlayerMovement = Player.GetComponent<PlayerMovement>();
    }


    private void OnTriggerEnter2D(Collider2D collision) 
    {if(!PlayerMovement.GetAlreadySwapped())return;
        if(collision.tag == PlayerTag&&isActive)
        {
            PlayerMovement.SwapGain();
            isActive = false;
            particles.Play();
            SoundManager.Instance.PlaySE(SESoundData.SE.SwapGet);
            anim.SetTrigger("ItemGet");
        }
    }
}
