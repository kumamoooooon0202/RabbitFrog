using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : Enemy
{

    [SerializeField] private float atackTime = 0.0f;


    void Start()
    {
        
    }

    void Update()
    {
        if (IsDeath) { return; }
        if (hp <= 0) { Death(); }
        if (serchFlag)
        {
            // 敵との距離を測る
            var distance = Vector3.Distance(transform.position, characterPos);
            // 攻撃範囲に入れば攻撃
            if (distance < attackRange)
            {
                Attack();
            }
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public override void Death()
    {
        IsDeath = true;
        gameObject.SetActive(false);
        //WGameSceneManager.LoadClearScene();
    }

    public override void Attack()
    {
        atackTime += Time.deltaTime;
        if (serchFlag && atackTime > attackInterval)
        {
            Debug.Log("攻撃");
            if (targetCharacter.myCharacteristic == characteristic.ironWall)
            {
                targetCharacter.hp -= 1;
            }
            else
            {
                targetCharacter.hp -= power;
            }
            atackTime = 0f;
        }

        // 攻撃している敵が死んだら再び索敵の開始
        if (targetCharacter.IsDeath) { serchFlag = false; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (serchFlag) { return; }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyTower")
        {
            // 敵の情報を取得
            targetCharacter = collision.GetComponent<Character>();
            serchFlag = true;
            characterPos = collision.transform.position;
            Debug.Log(targetCharacter + " : " + characterPos);
        }
    }
}
