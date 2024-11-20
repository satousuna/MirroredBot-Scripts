using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
 
public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement PlayerMovement;
    [SerializeField] private Rigidbody2D rb;
    private Animator anim = null;
    private Animator animClone = null;
    private bool AlreadyDead;
    [SerializeField] GameObject PlayerClone;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
        animClone = PlayerClone.GetComponent<Animator>();
    }
    void Update()
    {
        if(PlayerMovement.GetPlayerStop())return;//playerが止まっているときは再生しない
        float horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal != 0)
        {
            anim.SetBool("IsRunning", true);
            animClone.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
            animClone.SetBool("IsRunning", false);
        }

        if(rb.velocity.y > 0f)
        {
            anim.SetBool("IsJumping", true);
            animClone.SetBool("IsJumping", true);
        }
        else
        {
            anim.SetBool("IsJumping", false);
            animClone.SetBool("IsJumping", false);
        }
//アニメーション遷移条件"IsGrounded"判定
        if(PlayerMovement.IsGrounded())
        {
            anim.SetBool("IsGrounded", true);
            animClone.SetBool("IsGrounded", true);
        }
        else
        {
            anim.SetBool("IsGrounded", false);
            animClone.SetBool("IsGrounded", false);
        }

        if(AlreadyDead)return;
        if(PlayerMovement.GetDeath())
        {
            anim.SetTrigger("IsDying");
            AlreadyDead = true;
        }

    }
}