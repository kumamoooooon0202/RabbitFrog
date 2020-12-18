using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class OptionController : MonoBehaviour
{
    [SerializeField] private Canvas organizationCanvas;
    [SerializeField] private Canvas stageSelectCanvas;
    [SerializeField] private Canvas confirmCanvas;
    [SerializeField] private PreviewManager preMana;

    [SerializeField] private MaskControl organizationMask;
    [SerializeField] private RectTransform organization;
    [SerializeField] private MaskControl stageSelectMask;
    [SerializeField] private RectTransform stageSelect;

    GraphicRaycaster organization_Canvas_raycaster;
    GraphicRaycaster stageSelect_Canvas_raycaster;

    static public bool is_runing; // 巻物アニメーション、起動中 確認

    private bool is_organ_Open; // 編成画面が開いだ状態 確認
    private bool is_stage_Open; // ステージ選択画面が開いだ状態 確認

    void Start()
    {
        //organizationCanvas.enabled = false;
        //stageSelectCanvas.enabled = false;
        //confirmCanvas.enabled = false;

        //20/12/04 イゴンヒ
        //============================================================
        organization_Canvas_raycaster = organizationCanvas.GetComponent<GraphicRaycaster>();
        stageSelect_Canvas_raycaster = stageSelectCanvas.GetComponent<GraphicRaycaster>();


        organization_Canvas_raycaster.enabled = false;
        stageSelect_Canvas_raycaster.enabled = false;

        //
        organizationMask.image.fillAmount = 0;
        stageSelectMask.image.fillAmount = 0;

        is_runing = false;

        is_organ_Open = false;
        is_stage_Open = false;
        //============================================================
    }

    //================================
    //20/12/04 イゴンヒ
    /// <summary>
    /// 編成画面ボタンを押した時
    /// </summary>
    public void OnOpenOrganization()
    {
        //if (is_runing == true || is_organ_Open) return;
        if (is_runing == true) return;
        //is_organ_Open = true;

        //ステージ選択画面が開いた状態と編成画面が閉じた状態なら
        if (is_stage_Open == true && is_organ_Open == false)
        {
            is_organ_Open = true;
            stageSelect_Canvas_raycaster.enabled = false;
            organization_Canvas_raycaster.enabled = true;
            StartCoroutine(organizationMask.Close_Open(stageSelectMask, organization, stageSelect));//ステージ選択画面を閉じて編成画面を開く

            is_stage_Open = false;
        }
        //ステージ選択画面が閉じた状態ならステージ選択画面を開く
        else if (is_organ_Open == false)
        {
            is_organ_Open = true;
            stageSelect_Canvas_raycaster.enabled = false;
            organization_Canvas_raycaster.enabled = true;
            StartCoroutine(organizationMask.Open(organization));
        }

        //ステージ選択画面が開いた状態ならステージ選択画面を閉じる
        else if (is_organ_Open == true)
        {
            is_organ_Open = false;
            stageSelect_Canvas_raycaster.enabled = false;
            organization_Canvas_raycaster.enabled = true;
            StartCoroutine(organizationMask.Close(organization));
        }

        //================================
    }

    //================================
    //20/12/04 イゴンヒ
    /// <summary>
    /// ステージ選択画面を押した時
    /// </summary>
    public void OnOpenStageSelect()
    {
        //if (is_runing == true || is_stage_Open) return;
        if (is_runing == true) return;


        if (is_organ_Open == true && is_stage_Open == false)
        {
            is_stage_Open = true;
            organization_Canvas_raycaster.enabled = false;
            stageSelect_Canvas_raycaster.enabled = true;
            StartCoroutine(stageSelectMask.Close_Open(organizationMask, stageSelect, organization));

            is_organ_Open = false;
        }

        else if (is_stage_Open == false)
        {
            is_stage_Open = true;
            organization_Canvas_raycaster.enabled = false;
            stageSelect_Canvas_raycaster.enabled = true;
            StartCoroutine(stageSelectMask.Open(stageSelect));
        }

        else if (is_stage_Open == true)
        {
            is_stage_Open = false;
            organization_Canvas_raycaster.enabled = false;
            stageSelect_Canvas_raycaster.enabled = false;

            StartCoroutine(stageSelectMask.Close(stageSelect));
        }
        //================================
    }



    //public void OnOpenConfirm()
    //{
    //    stageSelectCanvas.rootCanvas.enabled = false;
    //    organizationCanvas.rootCanvas.enabled = false;
    //    confirmCanvas.rootCanvas.enabled = !confirmCanvas.rootCanvas.enabled;
    //}

}
