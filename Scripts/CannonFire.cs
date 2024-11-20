using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour
{
    [SerializeField] private float StartBulletInterval;
    [SerializeField] private float BulletInterval;
    [SerializeField] private GameObject CannonBullet;
    [SerializeField] private Transform BulletPoint;
    private Animator anim = null;
    void Start()
    {
        anim = GetComponent<Animator>();
        BulletInterval = StartBulletInterval;
    }

    void Update()
    {
        if(GManager.instance.IsPaused)return;
        if(BulletInterval <= 0)
        {
            anim.SetTrigger("Fire");
            SoundManager.Instance.PlaySE(SESoundData.SE.CannonShoot);//CannonShoot効果音
            Instantiate(CannonBullet,BulletPoint.position,BulletPoint.rotation);
            BulletInterval = StartBulletInterval;
        }
        else
        {
            BulletInterval -= Time.deltaTime;
        }
    }

}
