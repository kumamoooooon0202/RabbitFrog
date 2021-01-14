using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] private int myHandNumber;
    private Transform parentObject;
    private GameObject dragObject;
    private HandManager handManager;

    private Image dragImage;
    private Image souceImage;

    public Image characterIconImage = null;
    public Text costText = null;

    [SerializeField] private PreviewManager preMana;

    void Awake()
    {
        parentObject = transform.parent;
        handManager = FindObjectOfType<HandManager>();
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        CreateCharacterObject();
        dragObject.transform.position = GetMousePosition();
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        dragObject.transform.position = GetMousePosition();

        //＝＝＝＝＝＝＝
        if (is_Summonable()) { dragImage.color = new Color(souceImage.color.r, souceImage.color.g, souceImage.color.b, 1.0f); }
        else { dragImage.color = new Color(souceImage.color.r, souceImage.color.g, souceImage.color.b, 0.25f); }
        //イゴンヒ
       
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        gameObject.GetComponent<Image>().color = Vector4.one;
        Destroy(dragObject);
        var pos = Camera.main.ScreenToWorldPoint(pointerEventData.position);
        pos.z += 10.0f;

        if(is_Summonable()) handManager.CharacterSummon(pos, myHandNumber);
       
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerEnter == null) { return; }
        preMana.DisplayPreview();
        var cardInfo = DeckManager.deckObjects[myHandNumber].cardPoolObject;
        //var cardInfo = pointerEventData.pointerEnter.GetComponent<CardPoolObject>();
        preMana.nameText.text = cardInfo.character.characterName;
        preMana.costText.text = cardInfo.character.cost.ToString();
        preMana.characterImage.sprite = cardInfo.character.image;
        preMana.iconImage.sprite = cardInfo.character.characteristicIcon;
        preMana.explanationText.text = preMana.data.GetExplanation((int)cardInfo.character.myCardType);
    }

    /// <summary>
    /// Dragしたキャラクターの複製
    /// </summary>
    public void CreateCharacterObject()
    {
        Vector3 mousePos = Input.mousePosition;
        dragObject = new GameObject("DragObject");
        dragObject.transform.SetParent(parentObject);
        dragObject.transform.SetAsLastSibling();
        dragObject.transform.localPosition = mousePos;
        dragObject.transform.localScale = Vector3.one;

        CanvasGroup canvasGroup = dragObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        dragImage = dragObject.AddComponent<Image>();
        souceImage = GetComponent<Image>();

        dragImage.sprite = souceImage.sprite;
        dragImage.rectTransform.sizeDelta = souceImage.rectTransform.sizeDelta;
        dragImage.color = souceImage.color;
        dragImage.material = souceImage.material;
        dragImage.preserveAspect = true;

        gameObject.GetComponent<Image>().color = Vector4.one * 0.6f;
    }

    /// <summary>
    /// MousePositionの取得
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMousePosition()
    {
        Vector3 screenMousuPos = Input.mousePosition;
        screenMousuPos.z = 10f;
        Camera gameCamera = Camera.main;
        Vector3 worldMousePos = gameCamera.ScreenToWorldPoint(screenMousuPos);
        return worldMousePos;
    }

    //=======================================================
    //=======================================================
    /// <summary>
    /// 青い線内に入れてるがを判断する。
    /// </summary>
    /// <param name="pointerEventData_pos"></param>
    /// <returns>召喚が出来る範囲がを判断</returns>
    private bool is_Summonable()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (pos.x > handManager.start_screenLimit.x && pos.x < handManager.end_screenLimit.x &&
            pos.y > handManager.start_screenLimit.y && pos.y < handManager.end_screenLimit.y)
        {
            return true;
        }
        //mousePosition.y > 100.0f && mousePosition.y < 300.0f;
        else
        {
            return false;
        }
    }
    //イゴンヒ

}
