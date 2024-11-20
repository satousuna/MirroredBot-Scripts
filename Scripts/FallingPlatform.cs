using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private PlayerMovement PlayerMovement;
    BoxCollider2D PlatformCollider;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private Transform PlayerCheck;
    private Animator anim = null;
        private void Start() 
    {
        anim = GetComponent<Animator>();
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        PlatformCollider = GetComponent<BoxCollider2D>();
    }

    public bool PlayerOn()
    {
        return Physics2D.OverlapCircle(PlayerCheck.position, 0.2f, PlayerLayer);
    }
    private void Update() 
    {
        // Playerと重なっているかどうかを判定
        if (PlayerOn()&&PlayerMovement.IsGrounded())
        {
            anim.SetTrigger("Activate");//重なっているオブジェクトがPlayerなら、アニメーションを開始する
        }
    }

    private void Vanish()
    {
        PlatformCollider.enabled = false;
        int layer = LayerMask.NameToLayer(default);
        this.gameObject.layer = layer;
    }
    private void FallSE()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.PlatformFall);
    }
}
