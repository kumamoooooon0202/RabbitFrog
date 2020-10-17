﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmCanvas_Battle : MonoBehaviour
{
    [SerializeField]
    Canvas confirmcanvas_battle;

    [SerializeField]
    Image nextStage;

    [SerializeField]
    Tower Rabbit_Tower;

    [SerializeField]
    EnemyTower Frog_Tower;

    [SerializeField]
    Effect_Sketch effect_Sketch;

    void Start()
    {
        confirmcanvas_battle.rootCanvas.enabled = false;
    }

    void Update()
    {
        if (Rabbit_Tower.IsDeath)
        {
            confirmcanvas_battle.rootCanvas.enabled = true;
            nextStage.gameObject.SetActive(false);
        }

        if(Frog_Tower.IsDeath)
        {
            confirmcanvas_battle.rootCanvas.enabled = true;
            clear();
        }
    }

    //実行関数、EnventTrigger
    ///========================================================
    public void MoveTitle()
    {
        StartCoroutine(effect_Sketch.NextScene("TitleScene"));
        GetComponent<GraphicRaycaster>().enabled = false;
    }
    public void MoveOption()
    {
        StartCoroutine(effect_Sketch.NextScene("OptionScene"));
        GetComponent<GraphicRaycaster>().enabled = false;
    }
    public void NextStage()
    {
        StartCoroutine(effect_Sketch.NextScene(NextSceneName()));
        GetComponent<GraphicRaycaster>().enabled = false;
    }

    ///========================================================
    /// <summary>
    /// 現在のSceneによって次のSceneのNameを決める。
    /// </summary>
    /// <returns></returns>
    string NextSceneName()
    {
        string name = SceneManager.GetActiveScene().name;

        switch (name)
        {
            case "BattleFirst":
                return "BattleSecond";
            case "BattleSecond":
                return "BattleThird";
            case "BattleThird":
                return "BattleBoss";
            case "BattleBoss":
                return "ClearScene";
            default:
                return null;
        }

    }

    private void clear()
    {
        string name = SceneManager.GetActiveScene().name;

        switch (name)
        {
            case "BattleFirst":
                SaveData.StageClear[1] = true;
                break;
            case "BattleSecond":
                SaveData.StageClear[2] = true;
                break;
            case "BattleThird":
                SaveData.StageClear[3] = true;
                break;
            case "BattleBoss":
                //SaveData.StageClear[3] = true;
                break;
            default:
                break;
        }
    }

}
