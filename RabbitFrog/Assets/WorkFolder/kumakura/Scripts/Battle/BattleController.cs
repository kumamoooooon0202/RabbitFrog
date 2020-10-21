﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    [SerializeField] private float gameTime = 90;   // 残り時間
    [SerializeField] private Text timeText;
    [SerializeField] private Image summonGage;
    [SerializeField] private Text summonGageValText;
    [SerializeField] Enemy enemyTower;
    [SerializeField] Tower rabbitTower;

    private float summonGageVal = 5.0f;     // 召喚ゲージ　: 最大は10
    public float SummonGageVal
    {
        get { return summonGageVal; }
        set { summonGageVal = value; }
    }

    public List<GameObject> characterList = new List<GameObject>();

    void Start()
    {

    }

    void Update()
    {
        if (Time.timeScale == 0) { return; }
        // 制限時間の処理
        if (gameTime <= 0)
        {
            // 勝敗判定
            if (enemyTower.hp <= rabbitTower.hp)
            {
                // 勝利判定
            }
            if (rabbitTower.hp <= enemyTower.hp)
            {
                // 敗北判定
            }

            foreach (var chara in characterList)
            {
                chara.GetComponent<CharacterBase>().IsMove = false;
            }
            Time.timeScale = 0;
            return;
        }
        gameTime -= Time.deltaTime;
        timeText.text = gameTime.ToString("00");
        summonGageValText.text = Mathf.Floor(summonGageVal).ToString("0");

        summonGageVal = Mathf.Clamp(summonGageVal + Time.deltaTime / 3, 0.0f, 10.0f);

        // デバッグ用
        //-----------------------------------------------------------------------------

        if (Input.GetKeyDown(KeyCode.A))
        {
            summonGageVal = Mathf.Clamp(summonGageVal + 1.0f, 0.0f, 10.0f);

        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            summonGageVal = Mathf.Clamp(summonGageVal - 1.0f, 0.0f, 10.0f);
        }

        //-----------------------------------------------------------------------------

        summonGage.fillAmount = summonGageVal / 10.0f;

        #region チートコマンド
        if (Input.GetKey(KeyCode.Return))
        {
            // ボス即死
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var enemy = FindObjectOfType<EnemyTower>();
                enemy.hp = 0;
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                
            }
        }
        #endregion
    }
}
