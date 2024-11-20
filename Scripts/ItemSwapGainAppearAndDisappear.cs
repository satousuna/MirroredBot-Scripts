using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwapGainAppearAndDisappear : MonoBehaviour
{
    private string PlayerTag = "Player";
    [SerializeField] GameObject Player;
    private PlayerMovement PlayerMovement;
    [SerializeField] private GameObject ItemSwapGain;
    public ParticleSystem particles;

    private void Start() 
    {
        PlayerMovement = Player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(PlayerMovement.IsGrounded() && !ItemSwapGain.activeSelf)
        ItemSwapGain.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {if(!PlayerMovement.GetAlreadySwapped())return;//入れ替えしていないなら獲得しない
        if(other.tag == PlayerTag)
        {
            particles.Play();
            ItemSwapGain.SetActive(false);
        }
    }
}