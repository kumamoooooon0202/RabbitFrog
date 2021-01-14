using System.Collections;
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

    [SerializeField]
    Image win;

    private bool is_Click = false;

    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip tapSE;

    void Start()
    {
        confirmcanvas_battle.rootCanvas.enabled = false;
    }

    void Update()
    {
        //if (Rabbit_Tower.IsDeath)
        //{
        //    RabbitDeath();
        //}

        //if(Frog_Tower.IsDeath)
        //{
        //    FlogDeath();
        //}
    }

    public void RabbitDeath()
    {
        //confirmcanvas_battle.rootCanvas.enabled = true;
        nextStage.gameObject.SetActive(false);
        win.enabled = false;
    }

    public void FlogDeath()
    {
        //confirmcanvas_battle.rootCanvas.enabled = true;
        clear();
    }



    //実行関数、EnventTrigger
    ///========================================================
    public void MoveTitle()
    {
        audio.PlayOneShot(tapSE);
        StartCoroutine(effect_Sketch.NextScene("TitleScene"));
        GetComponent<GraphicRaycaster>().enabled = false;
    }
    public void MoveOption()
    {
        audio.PlayOneShot(tapSE);
        StartCoroutine(effect_Sketch.NextScene("OptionScene"));
        GetComponent<GraphicRaycaster>().enabled = false;
    }
    public void NextStage()
    {
        audio.PlayOneShot(tapSE);
        //StartCoroutine(effect_Sketch.NextScene(NextSceneName()));
        StartCoroutine(effect_Sketch.NextScene("ScenarioScene"));
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
                SaveData.StageClear[4] = true;
                break;
            default:
                break;
        }
    }

}
