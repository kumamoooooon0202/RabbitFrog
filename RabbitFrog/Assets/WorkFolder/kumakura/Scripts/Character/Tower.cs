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

    void Awake()
    {
        maxHp = hp;
    }

    void FixedUpdate()
    {
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
            if (targetEnemy.myCharacteristic == characteristic.ironWall)
            {
                targetEnemy.hp -= 1;
            }
            else
            {
                targetEnemy.hp -= power;
            }
            atackTime = 0f;
        }

        // 攻撃している敵が死んだら再び索敵の開始
        if (targetEnemy.IsDeath) { serchFlag = false; }
    }

    public override void Death()
    {
        IsDeath = true;
        // 編成画面に戻るか聞くUIの表示
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (serchFlag) { return; }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyTower")
        {
            // 敵の情報を取得
            targetEnemy = collision.GetComponent<Enemy>();
            serchFlag = true;
            enemyPos = collision.transform.position;
            Debug.Log(targetEnemy + " : " + enemyPos);
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyTower")
    //    {
    //        collision.transform.position -= new Vector3(Time.deltaTime * 4, 0, 0);
    //    }
    //}
}
