using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : CharacterBase
{
    [SerializeField, Header("攻撃範囲")] protected float attackRange = 1.5f;
    [SerializeField, Header("攻撃速度")] protected float attackInterval = 1.75f;
    [SerializeField] private float atackTime = 0.0f;
    private Enemy targetEnemy;
    private bool serchFlag = false;
    private Vector2 enemyPos;
    private int maxHp;

    private List<Enemy> targetEnemies = new List<Enemy>();

    public string attackTrigger = "AttackTrigger";
    public string isMove = "IsMove";
    public string isDeath = "IsDeath";

    void Awake()
    {
        maxHp = hp;
    }

    void FixedUpdate()
    {
        if (GetComponent<Animator>() != null)
        {
            characterAnim = GetComponent<Animator>();
        }
        hpText.text = hp.ToString("") + "/" + maxHp.ToString("");
    }

    void Update()
    {
        if (IsDeath) { return; }
        if (hp <= 0) { Death(); }
        if (serchFlag)
        {
            // 敵との距離を測る
            var distance = Vector3.Distance(transform.position, enemyPos);
            // 攻撃範囲に入れば攻撃
            if (distance < attackRange)
            {
                Attack();
            }
        }
    }

    public override void Attack()
    {
        atackTime += Time.deltaTime;
        if (serchFlag && atackTime > attackInterval)
        {
            Debug.Log("攻撃");
            if (characterAnim != null) { characterAnim.SetTrigger(attackTrigger); }
            atackTime = 0f;
        }

        // 攻撃している敵が死んだら再び索敵の開始
        //if (targetEnemy.IsDeath) { serchFlag = false; }
        // リストの中身のIsDeathが全部trueになったら移動再開
        if (targetEnemies[targetEnemies.Count - 1].IsDeath)
        {
            foreach (var enemy in targetEnemies)
            {
                if (enemy.IsDeath) { continue; }
                else { return; }
            }
            
            serchFlag = false;
        }
    }

    public void Damege()
    {
        foreach (var targetenemy in targetEnemies)
        {
            if (targetenemy.myCharacteristic == characteristic.ironWall)
            {
                if (!targetenemy.IsDeath)
                {
                    targetenemy.hp -= 1;
                    break;
                }
                else
                {
                    continue;
                }
                //foreach (var enemy in targetEnemies)
                //{
                //}
            }
            else
            {
                if (!targetenemy.IsDeath)
                {
                    targetenemy.hp -= power;
                    break;
                }
                else
                {
                    continue;
                }
            }

        }
    }

    public override void Death()
    {
        if (characterAnim != null) { characterAnim.SetTrigger(isDeath); }
        IsDeath = true;
        // 編成画面に戻るか聞くUIの表示
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (serchFlag) { return; }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyTower")
        {
            // 敵の情報を取得
            //targetEnemy = collision.GetComponent<Enemy>();
            targetEnemies.Add(collision.GetComponent<Enemy>());
            serchFlag = true;
            enemyPos = collision.transform.position;
        }
    }
}
