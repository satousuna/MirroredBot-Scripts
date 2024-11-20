using System.Collections;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public PlayerRunData Data;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float yvelocityreverse = -0.7f;//縦方向入れ替え時に速度をどれくらい引き継ぐか

    private bool isFacingRight = true;
    private float coyotetime = 0.1f;
    private float coyotetimecounter;
    private float jumpbuffertime = 0.2f;
    private float jumpbuffercounter;
    private float fallMultiplier = 3f;
    [SerializeField] private float fallClamp = -20f;
    [SerializeField] private float freezeDuration;
    private float ParticleSpeed;
    private bool IsDead;//死亡しているか
    private bool IsStopped;//停止しているか
    private bool Retry;
    private bool OnMovePlat;
    private bool CanJump;
    
    [SerializeField] private bool AlreadySwapped;//空中で入れ替え消費したか 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask MovingGroundLayer;
    private string MovingGroundTag = "MovingGround";
    private Animator anim = null;
    public ParticleSystem dust;
    public ParticleSystem explosion;
    private Transform platformTransform;
    private Vector3 lastPlatformPosition;
    [SerializeField] private GameObject LineVertical;
    [SerializeField] private GameObject LineHorizontal;
    [SerializeField] private GameObject PlayerClone;
    [SerializeField] private GameObject ParticleTrail;
    [SerializeField] private GameObject DarkScreen;
    [SerializeField] private GameObject CountVer;
    [SerializeField] private GameObject CountHor;
    [SerializeField] private GameObject PauseCanvas;
    private MovingPlatform MovePlat = null;
    private CloneDetection CloneDetection;
    private SpriteRenderer SpriteRenderer;
    private CountVerText CountVerText;
    private CountHorText CountHorText;

    
    void Start()
    {
        anim = GetComponent<Animator>();
        CloneDetection = PlayerClone.GetComponent<CloneDetection>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        CountVerText =  CountVer.GetComponent<CountVerText>();
        CountHorText =  CountHor.GetComponent<CountHorText>();
    }

    void Update()
    {
        if (IsDead)return;
        if (IsStopped)return;
//横方向の入力受け取り
        horizontal = Input.GetAxisRaw("Horizontal");
        if (IsGrounded())//接地時
        {
            SwapGain();//入れ替え回数回復
            coyotetimecounter = coyotetime;//コヨーテタイムカウントしない
        }
        else
        {
            coyotetimecounter -= Time.deltaTime;//コヨーテタイムカウント開始
        }
        if (coyotetimecounter > 0f)
        {
            CanJump = true;
        }
        else
        {
            CanJump = false;
        }

        if(Input.GetButtonDown("Jump"))
        {
            jumpbuffercounter = jumpbuffertime;
        }
        else
        {
            jumpbuffercounter -= Time.deltaTime;
        }

//コヨーテタイム中(接地)&ジャンプバッファタイム中(ジャンプボタン押下)押下時間に応じてジャンプ
        if (coyotetimecounter > 0f && jumpbuffercounter > 0f)
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.Jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpbuffercounter = 0f;
            CreateDust();
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyotetimecounter = 0f;
        }
//落下速度を早くする
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        if (rb.velocity.y < fallClamp)
        {
            rb.velocity = new Vector2(rb.velocity.x, fallClamp);
        }

        GenerateLine();

        CloneMovement();

        CloneSwap();

        Flip();

        Pause();
        
    }

//横移動
    private void FixedUpdate()
    {
        Run();
        Vector2 addVelocity = Vector2.zero;
        if (MovePlat != null&&OnMovePlat)
        {
            addVelocity = MovePlat.GetVelocity();
            rb.velocity += addVelocity/3;
        }
    }
        private void Run()
	{
		//目標速度設定
		float targetSpeed = horizontal * Data.runMaxSpeed;
		float accelRate;

		//加速中か減速中かどうかに基づいて加速値を取得(空中時は別の乗数)
		if (coyotetimecounter > 0)
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
		else
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;

		//希望通りの方向に目標速度以上のスピードで動いていたら減速させない
		if(Data.doConserveMomentum && Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && coyotetimecounter > 0)
		{
			//運動量を維持
			accelRate = 0; 
		}

		///現在の速度と目標速度の差を計算
		float speedDif = targetSpeed - rb.velocity.x;
		//x方向の力を計算し加える
		float movement = speedDif * accelRate;
		//ベクトルに変換しrigidbodyに適用
		rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
	}

//接地判定
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer | MovingGroundLayer);
    }
        public bool IsMovingGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, MovingGroundLayer);
    }

//入力が向きに対しマイナス方向の時キャラクターを振り向かせる
    private void Flip()
    {
        if (IsDead)return;
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            if (IsGrounded())
            {
            SoundManager.Instance.PlaySE(SESoundData.SE.Flip);
            CreateDust();
            }
            if(LineVertical.activeSelf||LineHorizontal.activeSelf)//Lineが出ているとき分身の振り向きも制御
            {
                Vector3 CloneScale = PlayerClone.transform.localScale;
                CloneScale.x *= -1;//分身が振り返る
                PlayerClone.transform.localScale = CloneScale;//更新
            }
        }
    }
    
    
//Dust発生させる
        private void CreateDust()
    {
        dust.Play();
    }
    
// プレイヤーが衝突したときの処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトのタグが"Death"で停止中でなかった場合
        if (collision.gameObject.CompareTag("Death")&&!IsStopped)
        {
            PlayerDeath();//死亡
        }
        if (collision.collider.tag == MovingGroundTag)
        {
            //動く床に触れている
            MovePlat = collision.gameObject.GetComponent<MovingPlatform>();
            platformTransform = collision.transform;
            lastPlatformPosition = platformTransform.position;  // 床の初期位置を記録
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
            if (collision.collider.tag == MovingGroundTag&&IsGrounded())
        {
            //動く床に乗っている
            OnMovePlat = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
    if (collision.collider.tag == MovingGroundTag)
        {
            //動く床から離れた
            MovePlat = null;
            OnMovePlat = false;
        }
    }

//死亡直後の処理
    public void PlayerDeath()
    {
        PlayerClone.SetActive(false);
        if(!IsDead)
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.Damage);//ダメージ音
        }
        IsDead = true;//死亡しプレイヤーの動作を止める
        rb.velocity = new Vector2(0,0);
        rb.isKinematic = true;
        GManager.instance.DeathCount += 1;//死亡カウント加算
        Debug.Log("死");
    }
//死亡アニメーションが終わったとき呼び出す
    public void OnDeathAnimationEnd()
    {
        SpriteRenderer.color =  new Color32(0, 0 ,0 ,0);//キャラ透明にする
        SoundManager.Instance.PlaySE(SESoundData.SE.Death);//爆発音
        explosion.Play();//爆発
        Debug.Log("爆発");
        Retry = true;//シーン遷移(リトライ)変数を送る
    }

    public bool GetDeath()//他のobjectにプレイヤーの死亡状況を送るときに呼び出す
    {
        return IsDead;
    }
    public bool GetRetry()//他のobjectにシーン遷移(リトライ)変数を送るときに呼び出す
    {
        return Retry;
    }

    public bool GetAlreadySwapped()
    {
        return AlreadySwapped;
    }



    private void GenerateLine()
    {
        if(Input.GetButtonDown("Fire1"))//左クリックで縦方向の線を生成
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.Line);//ライン効果音
            LineVertical.SetActive(true);//線を生成
            Vector2 LinePos = transform.position;//線の位置LinePosの初期値に現在位置を代入
            LineVertical.transform.position = LinePos;//縦線の位置にLinePosを代入
            LineHorizontal.SetActive(false);//横線があれば消す
            PlayerClone.SetActive(true);//分身を生成
            PlayerClone.transform.position = LinePos;
            Vector2 CloneScale = PlayerClone.transform.localScale;//分身の初期の向きを設定
            if(isFacingRight)//Playerが右向きの時
            {
                CloneScale.x = -1;//クローンを左向きに
                CloneScale.y = 1;//クローンを上向きに
            }
            else//Playerが左向きの時
            {
                CloneScale.x = 1;//クローンを右向きに
                CloneScale.y = 1;//クローンを上向きに
            }
            PlayerClone.transform.localScale = CloneScale;//更新
        }

        if(Input.GetButtonDown("Fire2"))//右クリックで横方向の線を生成
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.Line);//ライン効果音
            LineHorizontal.SetActive(true);
            Vector2 LinePos = transform.position;
            LineHorizontal.transform.position = LinePos;
            LineVertical.SetActive(false);
            PlayerClone.SetActive(true);
            PlayerClone.transform.position = LinePos;
            Vector2 CloneScale = PlayerClone.transform.localScale;//分身の初期の向きを設定
            if(isFacingRight)//Playerが右向きの時
            {
                CloneScale.x = 1;//クローンを右向きに
                CloneScale.y = -1;//クローンを下向きに
            }
            else//Playerが左向きの時
            {
                CloneScale.x = -1;//クローンを左向きに
                CloneScale.y = -1;//クローンを下向きに
            }
            PlayerClone.transform.localScale = CloneScale;//更新
        }
    }
    private void CloneMovement()//分身の動きを制御
    {
        if(LineVertical.activeSelf)
        {
            Vector2 ClonePosition = PlayerClone.transform.position;
            float VerticalDistance = LineVertical.transform.position.x - transform.position.x;//線とプレイヤーの間の距離を取得
            ClonePosition.x = LineVertical.transform.position.x + VerticalDistance;//線のx軸座標に距離を加算
            ClonePosition.y = transform.position.y;//y座標はプレイヤーと同じ
            PlayerClone.transform.position = ClonePosition;
        }

        else if(LineHorizontal.activeSelf)
        {
            Vector2 ClonePosition = PlayerClone.transform.position;
            float HorizontalDistance = LineHorizontal.transform.position.y - transform.position.y;
            ClonePosition.y = LineHorizontal.transform.position.y + HorizontalDistance;
            ClonePosition.x = transform.position.x;
            PlayerClone.transform.position = ClonePosition;
        }
    }

    private void CloneSwap()
    {
        if(Input.GetButtonDown("Fire3") && LineHorizontal.activeInHierarchy || Input.GetButtonDown("Fire3") && LineVertical.activeInHierarchy)
        {
            if(!CloneDetection.GetSwappable())//クローンが入れ替えできない位置にあるか
            {
                return;
            }
            if(AlreadySwapped)//すでに空中で入れ替えしたか
            {
                return;
            }
            if(LineHorizontal.activeInHierarchy)
            {
                if(CountHorText.GetHorCounter() == 0)return;
            }
            if(LineVertical.activeInHierarchy)
            {
                if(CountVerText.GetVerCounter() == 0)return;
            }
            ParticleTrail.SetActive(true);
            StartCoroutine(FreezeForSeconds(freezeDuration));
            if(LineHorizontal.activeInHierarchy)//縦方向入れ替え時
            {
                CountHorText.ReduceHorCounter();
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * yvelocityreverse);//分身の縦方向慣性を引き継ぐ
            }
            else
            {
                CountVerText.ReduceVerCounter();
            }
            if(!IsGrounded())//接地していないとき
            {
                AlreadySwapped = true;
                SpriteRenderer.color =  new Color32(140, 140 ,140 ,255);
            }
            else if(IsMovingGrounded())
            {
            Collider2D overlappingColliders = Physics2D.OverlapCircle(groundCheck.position, 0.3f, MovingGroundLayer);//動く床に触れている
            MovePlat = overlappingColliders.gameObject.GetComponent<MovingPlatform>();
            platformTransform = overlappingColliders.transform;
            lastPlatformPosition = platformTransform.position;  // 床の初期位置を記録
            OnMovePlat = true;
            }
        }
    }
        IEnumerator FreezeForSeconds(float freezeDuration)
    {
        DarkScreen.SetActive(true);
        ParticleTrail.transform.position = this.transform.position;//初期位置を反映
        IsStopped = true;
        // 画面停止
        Time.timeScale = 0f;
        // freezeDurationの間、指定オブジェクトを動かす
        float timer = 0f;
        SoundManager.Instance.PlaySE(SESoundData.SE.SwapTrail);
        while (timer < freezeDuration)
        {
            if(GManager.instance.IsPaused == false)
            {
            timer += Time.unscaledDeltaTime;
            float distanceToTarget = Vector2.Distance(this.transform.position, PlayerClone.transform.position);
            // freezeDuration秒でその距離を移動するための速度を計算
            ParticleSpeed = distanceToTarget / freezeDuration;
            MoveTowardsTarget();
            yield return null;
            }
        }
        ParticleTrail.SetActive(false);
        Time.timeScale = 1f;
        IsStopped = false;
        DarkScreen.SetActive(false);
        Vector2 PlayerPosition = transform.position;//プレイヤーの座標を代入
        Vector2 ClonePosition = PlayerClone.transform.position;//クローンの座標を代入
        PlayerClone.transform.position = PlayerPosition;//クローンの座標を反映
        transform.position = ClonePosition;//プレイヤーの座標を反映
    }

        void MoveTowardsTarget()
    {
            // 現在の位置から分身の位置に向かって移動
            ParticleTrail.transform.position = Vector2.MoveTowards(
                ParticleTrail.transform.position, 
                PlayerClone.transform.position, 
                ParticleSpeed * Time.unscaledDeltaTime
            );
    }

    private void Pause()//ポーズ画面を開く
    {
        if (Input.GetButtonDown("Pause"))//ESCキーでポーズ画面開く
        {if (!GManager.instance.IsPaused&&!IsStopped)
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Pause);
                PlayerStop();
                PauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void SwapGain()//入れ替え回数回復
    {
        SpriteRenderer.color =  new Color32(255, 255 ,255 ,255);
        AlreadySwapped = false;
    }

    public void PlayerStop()
    {
        IsStopped = true;
    }

    public void PlayerStart()
    {
        IsStopped = false;
    }

    public bool GetPlayerStop()
    {
        return IsStopped;
    }
    
    public bool GetCanJump()
    {
        return CanJump;
    }

}