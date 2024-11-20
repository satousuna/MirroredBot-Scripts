using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisOutMask : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Transform Transform;
    private PlayerMovement PlayerMovement;
    void Start()
    {
        Player.GetComponent<Transform>();
        PlayerMovement = Player.GetComponent<PlayerMovement>();
    }

    private void Update() 
    {
        if (PlayerMovement.GetRetry())
        {
        transform.position = Player.transform.position;//Player死亡時にPlayerの位置をサーチ
        }
    }

}
