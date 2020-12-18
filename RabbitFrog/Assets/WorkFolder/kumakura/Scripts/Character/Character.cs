using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : CharacterBase
{
    [Header("移動速度")] public float moveSpeed;               // 移動速度
    [Header("召喚数")] public int summonVol;                   // 召喚数
    [Header("コスト")] public int cost;                        // コスト
    [Header("攻撃方法")] public AttackMethod myAttackMethod;   // 攻撃方法
    [SerializeField, Header("攻撃範囲")] protected float attackRange = 1.5f;
    [SerializeField, Header("攻撃速度")] protected float attackInterval = 1.75f;


    protected Vector2 enemyPos;
    protected bool serchFlag = false;
    protected float atackTime = 0.0f;
    protected Enemy targetEnemy;

    private int maxHp;

    //// 爆発の為の変数
    //[SerializeField] private Collider2D explosionCol;
    //private List<CharacterBase> targetCharacter = new List<CharacterBase>();
    //private bool explosionFlag;

    // Animation変数
    public Animator characterAnim;
    public string attackTrigger = "AttackTrigger";
    public string isMove = "IsMove";
    public string isDeath = "IsDeath";

    public enum AttackMethod
    {
        shortDistance,
        middleDistance,
        longDistance,
    }

    void Awake()
    {
        if (GetComponent<Animator>() != null)
        {
            characterAnim = GetComponent<Animator>();
        }
        maxHp = hp;
    }

    void FixedUpdate()
    {
        hpText.text = hp.ToString("") + "/" + maxHp.ToString("");
    }

    /// <summary>
    /// キャラの移動関数
    /// </summary>
    /// <param name="speed"></param>
    public virtual void CharacterMove(float speed)
    {
        if (IsMove)
        // 敵の索敵が出来れていれば敵に近づく処理
        if (serchFlag)
        {
            // 敵との距離を測る
            var distance = Vector3.Distance(transform.position, enemyPos);
            // 攻撃範囲に入れば攻撃
            if (distance < attackRange)
            {
                Attack();
                return;
            }
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, enemyPos, speed);
        }
        else
        {
            // 索敵状態であればひたすら歩く処理
            transform.Translate(-speed, 0, 0);
            if (characterAnim != null) { characterAnim.SetBool(isMove, true); }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (myCardType == CardType.thunderGod) { return; }
        //if (explosionFlag)
        //{
        //    targetCharacter.Add(GetComponent<CharacterBase>());
        //    Debug.Log("取得");
        //    Explosion();
        //}
        if (serchFlag) { return; }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyTower")
        {
            // 敵の情報を取得
            targetEnemy = collision.GetComponent<Enemy>();
            //targetEnemies.Add(collision.GetComponent<Enemy>());
            // 敵を発見したのでこれ以上他の敵と接触しないための処理
            serchFlag = true;
            // 索敵した敵のPositionを格納
            enemyPos = collision.transform.position;
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    public override void Attack()
    {
        //if (characterAnim != null) { characterAnim.SetBool(isMove, false); }
        atackTime += Time.deltaTime;
        if (serchFlag == true && atackTime > attackInterval)
        {
            Debug.Log("攻撃");
            if (characterAnim != null) { characterAnim.SetTrigger(attackTrigger); }
            switch (targetEnemy.myCharacteristic)
            {
                // 敵の特徴 : 無し
                // 敵の特徴 : 俊足
                // 敵の特徴 : 爆発
                // 敵の特徴 : 感電
                case characteristic.none:
                case characteristic.quickness:
                case characteristic.explosion:
                case characteristic.electricShock:
                    targetEnemy.hp -= power;
                    break;

                // 敵の特徴 : 隠密
                case characteristic.covert:
                    // 自身の攻撃方法が中距離、遠距離であれば攻撃不可
                    if (this.myAttackMethod == AttackMethod.longDistance || this.myAttackMethod == AttackMethod.middleDistance)
                    {
                        Debug.Log("隠密だから攻撃できないよ！");
                        break;
                    }
                    targetEnemy.hp -= power;
                    break;

                // 敵の特徴 : 鉄壁
                case characteristic.ironWall:
                    // 自身の特徴が俊足であれば鉄壁を無視
                    if (this.myCharacteristic == characteristic.quickness)
                    {
                        Debug.Log("鉄壁無視するよ！");
                        break;
                    }
                    targetEnemy.hp -= 1;
                    break;

                default:
                    Debug.LogError("特徴が不適切です");
                    break;
            }
            
            atackTime = 0f;
        }

        // 攻撃している敵が死んだら再び索敵の開始
        if (targetEnemy.IsDeath) { serchFlag = false; }
    }

    /// <summary>
    /// キャラの死亡
    /// </summary>
    public override void Death()
    {
        // 特徴が爆発ならばここで爆発をする
        //if (myCharacteristic == characteristic.explosion) { explosionFlag = true;}

        // ここで死亡アニメーションの再生

        IsDeath = true;
        gameObject.SetActive(false);

    }

    /// <summary>
    /// 爆発
    /// </summary>
    public void Explosion(Collider2D[] targetCharacter)
    {
        //取得したキャラクターのHPを減らす処理
        //（201030）イゴンヒ修正
        foreach (var chara in targetCharacter)
        {
            //（201127）イゴンヒ修正　敵たけ影響される
            if (chara.gameObject.tag == "Enemy")
            {
                chara.gameObject.GetComponent<CharacterBase>().hp -= 10;
            }
        }
        IsDeath = true;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 感電
    /// </summary>
    /// <param name="enemy">対象の敵</param>
    public void ElectricShock(Enemy enemy)
    {
        enemy.IsMove = false;
        enemy.Invoke("IsMoveHealing", 3f);
    }

    /// <summary>
    /// 感電からの回復
    /// </summary>
    public void IsMoveHealing()
    {
        IsMove = true;
    }
}
