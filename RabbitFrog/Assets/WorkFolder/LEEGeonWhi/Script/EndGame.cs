using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    Canvas endGameCanvas;

    [SerializeField]
    Canvas confirmcanvas_battle;

    [SerializeField]
    Tower Rabbit_Tower;

    [SerializeField]
    EnemyTower Frog_Tower;

    [SerializeField]
    BattleController battleController;

    //201127 イゴンヒ
    [SerializeField]
    GameObject PauseUI;

    //==================================
    //Panel用イメージ
    [SerializeField]
    GameObject Fail;
    [SerializeField]
    GameObject TimeOut;
    [SerializeField]
    GameObject Clear;
    //==================================

    private bool is_Click;

    void Start()
    {
        is_Click = false;
        endGameCanvas.rootCanvas.enabled = false;
        PauseUI.SetActive(true);
    }

    void Update()
    {
        //負けたら
        if (Rabbit_Tower.IsDeath && !is_Click)
        {
            endGameCanvas.rootCanvas.enabled = true;
            is_Click = true;
            Fail.SetActive(true);
            PauseUI.SetActive(false);
        }

        //勝ったら
        else if (Frog_Tower.IsDeath && !is_Click)
        {
            endGameCanvas.rootCanvas.enabled = true;
            is_Click = true;
            Clear.SetActive(true);
            PauseUI.SetActive(false);

        }

        //TimeOut
        else if (battleController.is_Time_out && !is_Click)
        {
            endGameCanvas.rootCanvas.enabled = true;
            is_Click = true;
            TimeOut.SetActive(true);
            PauseUI.SetActive(false);

        }
    }

    //実行関数、EnventTrigger
    ///========================================================
    public void Fail_img()
    {
        confirmcanvas_battle.enabled = true;
        endGameCanvas.rootCanvas.enabled = false;
    }

    public void TImeOut_img()
    {
        confirmcanvas_battle.enabled = true;
        endGameCanvas.rootCanvas.enabled = false;
    }

    public void Clear_img()
    {
        confirmcanvas_battle.enabled = true;
        endGameCanvas.rootCanvas.enabled = false;
    }
    ///========================================================
}
