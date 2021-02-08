using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : Enemy
{

    [SerializeField] private float atackTime = 0.0f;

    private List<CharacterBase> targetCharacters = new List<CharacterBase>();
    


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

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("------------------------");
            foreach (var i in targetCharacters)
            {
                Debug.Log("name" + i);
                Debug.Log("isDeath" + i.IsDeath);
            }
            Debug.Log("------------------------");
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public override void Death()
    {
        if (characterAnim != null) { characterAnim.SetTrigger(isDeath); }
        IsDeath = true;
        gameObject.SetActive(false);
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
        //if (targetCharacter.IsDeath) { serchFlag = false; }
        if (targetCharacters[targetCharacters.Count - 1].IsDeath)
        {
            foreach (var chara in targetCharacters)
            {
                if (chara.IsDeath) { continue; }
                else { return; }
            }
            serchFlag = false;
        }
    }

    public override void Damege()
    {
        foreach (var chara in targetCharacters)
        {
            if (chara.myCharacteristic == characteristic.ironWall)
            {
                if (!chara.IsDeath)
                {
                    chara.hp -= 1;
                    break;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                if (!chara.IsDeath)
                {
                    chara.hp -= power;
                    break;
                }
                else
                {
                    continue;
                }
            }
        }


        //if (targetCharacter.myCharacteristic == characteristic.ironWall)
        //{
        //    targetCharacter.hp -= 1;
        //}
        //else
        //{
        //    targetCharacter.hp -= power;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (serchFlag) { return; }
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Tower")
        {
            // 敵の情報を取得
            //targetCharacter = collision.GetComponent<Character>();
            targetCharacters.Add(collision.GetComponent<Character>());
            serchFlag = true;
            characterPos = collision.transform.position;
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (serchFlag) { return; }
    //    if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Tower")
    //    {
    //        // 敵の情報を取得
    //        targetCharacter = collision.GetComponent<Character>();
    //        serchFlag = true;
    //        characterPos = collision.transform.position;
    //        Debug.Log(targetCharacter + " : " + characterPos);
    //    }
    //}
}
