using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class ThunderGodCharacter : Character
{
    private List<Enemy> targetEnemies = new List<Enemy>();


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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("-------雷神の情報-------");
            Debug.Log("TargetEnemies");
            foreach (var i in targetEnemies)
            {
                Debug.Log(i);
            }
            Debug.Log("SerchFlag : " + serchFlag);
            Debug.Log("EnemyPos : " + enemyPos);
            Debug.Log("------------------------");
        }
    }

    public override void CharacterMove(float speed)
    {
        if (IsMove)
            // 敵の索敵が出来れていれば敵に近づく処理
            if (serchFlag)
            {
                //// 敵との距離を測る
                //var distance = Vector3.Distance(transform.position, enemyPos);
                //// 攻撃範囲に入れば攻撃
                //if (distance < attackRange)
                //{
                    Attack();
                //    return;
                //}
                //gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, enemyPos, speed);
            }
            else
            {
                // 索敵状態であればひたすら歩く処理
                transform.Translate(-speed, 0, 0);
                if (characterAnim != null) { characterAnim.SetBool(isMove, true); }
            }
    }

    public override void Attack()
    {
        //if (characterAnim != null) { characterAnim.SetBool(isMove, false); }
        atackTime += Time.deltaTime;
        if (serchFlag == true && atackTime > attackInterval)
        {
            Debug.Log("攻撃");
            if (characterAnim != null) { characterAnim.SetTrigger(attackTrigger); }
            foreach (var enemy in targetEnemies)
            {
                if (enemy.IsDeath) { continue; }
                switch (enemy.myCharacteristic)
                {
                    // 敵の特徴 : 無し
                    // 敵の特徴 : 俊足
                    // 敵の特徴 : 爆発
                    // 敵の特徴 : 感電
                    case characteristic.none:
                    case characteristic.quickness:
                    case characteristic.explosion:
                    case characteristic.electricShock:
                        enemy.hp -= power;
                        // 感電の処理
                        ElectricShock(enemy);
                        break;

                    // 敵の特徴 : 隠密
                    case characteristic.covert:
                        // 自身の攻撃方法が中距離、遠距離であれば攻撃不可
                        Debug.Log("隠密だから攻撃できないよ！");
                        break;

                    // 敵の特徴 : 鉄壁
                    case characteristic.ironWall:
                        enemy.hp -= 1;
                        ElectricShock(enemy);
                        break;

                    default:
                        Debug.LogError("特徴が不適切です");
                        break;
                }
            }
            
            atackTime = 0f;
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (serchFlag) { return; }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyTower")
        {
            // 敵の情報を取得
            if (collision.GetComponent<Enemy>().myCharacteristic == characteristic.covert) { return; }
            targetEnemies.Add(collision.GetComponent<Enemy>());
            //serchFlag = true;
            // 索敵した敵のPositionを格納
            //enemyPos = collision.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyTower")
        {
            serchFlag = true;
        }
        else
        {
            serchFlag = false;
        }
    }
}
