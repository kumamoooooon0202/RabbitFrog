﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderGodCharacter : Character
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDeath) { return; }
        if (hp <= 0) { Death(); }
        CharacterMove(moveSpeed);
    }

    public override void CharacterMove(float speed)
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
            }
    }

    public override void Attack()
    {
        Debug.Log("aaaaa");
        atackTime += Time.deltaTime;
        if (serchFlag == true && atackTime > attackInterval)
        {
            Debug.Log("攻撃");

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
                    Debug.Log("隠密だから攻撃できないよ！");
                    break;

                // 敵の特徴 : 鉄壁
                case characteristic.ironWall:
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
}
