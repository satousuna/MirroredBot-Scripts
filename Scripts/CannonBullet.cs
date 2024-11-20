using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    [SerializeField] private float BulletErase = 3f;
    [SerializeField] private float speed;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        if(BulletErase <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            BulletErase -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.tag == "Player")
            {
                Destroy(gameObject);//このゲームオブジェクトを消滅させる
            }
    }
}   
