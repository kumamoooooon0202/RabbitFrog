using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase
{
    [Header("移動速度")] public float moveSpeed;               // 移動速度
    [Header("召喚数")] public int summonVol;                   // 召喚数
    [Header("攻撃方法")] public AttackMethod myAttackMethod;   // 攻撃方法
    [Header("攻撃範囲")] public float attackRange = 1.5f;
    [SerializeField, Header("攻撃速度")] private float attackInterval = 1.75f;

    public bool targetFlag = false;

    protected Vector2 characterPos;
    [SerializeField] protected bool serchFlag = false;
    protected float time = 0.0f;
    protected CharacterBase targetCharacter;

    private int maxHp;

    public enum AttackMethod
    {
        shortDistance,
        middleDistance,
        longDistance,
    }

    void Awake()
    {
        maxHp = hp;
    }

    void FixedUpdate()
    {
        hpText.text = hp.ToString("") + "/" + maxHp.ToString("");
    }

    public void EnemyMove(float speed)
    {
        if (IsMove)
        if (serchFlag)
        {
            var distance = Vector3.Distance(transform.position, characterPos);
            if (distance < attackRange)
            {
                Attack();
                return;
            }
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, characterPos, speed);
        }
        else
        {
            transform.Translate(speed, 0f, 0f);
        }
    }

    public override void Attack()
    {
        time += Time.deltaTime;
        if (serchFlag&& time > attackInterval)
        {
            Debug.Log("攻撃");
            switch (targetCharacter.myCharacteristic)
            {
                // 敵の特徴 : 無し
                // 敵の特徴 : 俊足
                // 敵の特徴 : 爆発
                // 敵の特徴 : 感電
                case characteristic.none:
                case characteristic.quickness:
                case characteristic.explosion:
                case characteristic.electricShock:
                    targetCharacter.hp -= power;
                    break;

                // 敵の特徴 : 隠密
                case characteristic.covert:
                    // 自身の攻撃方法が中距離、遠距離であれば攻撃不可
                    if (this.myAttackMethod == AttackMethod.longDistance || this.myAttackMethod == AttackMethod.middleDistance)
                    {
                        Debug.Log("隠密だから攻撃できないよ！");
                        break;
                    }
                    targetCharacter.hp -= power;
                    break;

                // 敵の特徴 : 鉄壁
                case characteristic.ironWall:
                    // 自身の特徴が俊足であれば鉄壁を無視
                    if (this.myCharacteristic == characteristic.quickness)
                    {
                        Debug.Log("鉄壁無視するよ！");
                        break;
                    }
                    targetCharacter.hp -= 1;
                    break;

                default:
                    Debug.LogError("特徴が不適切です");
                    break;
            }
            time = 0f;
        }

        // 攻撃している敵が死んだら再び索敵の開始
        if (targetCharacter.IsDeath) { serchFlag = false; }
    }

    public override void Death()
    {
        IsDeath = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (serchFlag) { return; }
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Tower")
        {
            targetCharacter = collision.GetComponent<CharacterBase>();
            serchFlag = true;
            characterPos = collision.transform.position;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (serchFlag)
        {
            serchFlag = false;
        }

    }

    /// <summary>
    /// 感電からの回復
    /// </summary>
    public void IsMoveHealing()
    {
        IsMove = true;
        Debug.Log("感電から回復");
    }
}
