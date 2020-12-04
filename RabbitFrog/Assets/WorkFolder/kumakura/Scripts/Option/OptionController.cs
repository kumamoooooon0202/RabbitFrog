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

    [SerializeField] private OrganizationMask organizationMask;
    [SerializeField] private Organization organization;
    [SerializeField] private StageSelectMask stageSelectMask;
    [SerializeField] private StageSelect stageSelect;

    GraphicRaycaster organization_Canvas_raycaster;
    GraphicRaycaster stageSelect_Canvas_raycaster;

    Image organizationButton;
    Image stageSelectButton;

    static public bool is_runing; // 巻物アニメーション、起動中 確認

    private bool is_organ_Open; // 編成画面が開いだ状態 確認
    private bool is_stage_Open; // ステージ選択画面が開いだ状態 確認

    void Start()
    {
        //organizationCanvas.enabled = false;
        //stageSelectCanvas.enabled = false;
        //confirmCanvas.enabled = false;

        //20/11/23 イゴンヒ
        //============================================================
        organization_Canvas_raycaster = organizationCanvas.GetComponent<GraphicRaycaster>();
        stageSelect_Canvas_raycaster = stageSelectCanvas.GetComponent<GraphicRaycaster>();

        organizationButton = organization.GetComponent<Image>();
        stageSelectButton = stageSelect.GetComponent<Image>();

        organization_Canvas_raycaster.enabled = true;
        stageSelect_Canvas_raycaster.enabled = false;

        organizationMask.image.fillAmount = 1;
        stageSelectMask.image.fillAmount = 0;

        is_runing = false;

        is_organ_Open = true;
        is_stage_Open = false;
        //============================================================
    }

    //================================
    // 20-11-23 イゴンヒ
    /// <summary>
    /// 編成画面ボタンを押した時
    /// </summary>
    public void OnOpenOrganization()
    {
        if (is_runing == true || is_organ_Open) return;

        is_organ_Open = true;
        //organizationMask.Open_animation();
        StartCoroutine(organizationMask.Open(stageSelectMask));
        stageSelect_Canvas_raycaster.enabled = false;
        organization_Canvas_raycaster.enabled = true;

        is_stage_Open = false;
        //================================
    }

    //================================
    // 20-11-23 イゴンヒ
    /// <summary>
    /// ステージ選択画面を押した時
    /// </summary>
    public void OnOpenStageSelect()
    {
        if (is_runing == true || is_stage_Open) return;

        is_stage_Open = true;

        StartCoroutine(stageSelectMask.Open(organizationMask));
        organization_Canvas_raycaster.enabled = false;
        stageSelect_Canvas_raycaster.enabled = true;
        
        is_organ_Open = false;
        //================================
    }



    //public void OnOpenConfirm()
    //{
    //    stageSelectCanvas.rootCanvas.enabled = false;
    //    organizationCanvas.rootCanvas.enabled = false;
    //    confirmCanvas.rootCanvas.enabled = !confirmCanvas.rootCanvas.enabled;
    //}

}
