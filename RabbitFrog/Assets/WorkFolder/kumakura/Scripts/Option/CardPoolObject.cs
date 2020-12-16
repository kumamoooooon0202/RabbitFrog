using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class CardPoolObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Transform parentObject;
    private GameObject dragObject;
    public Character character;
    public PreviewManager preMana;
    public Image characterIconImage = null;
    public Text costText = null;

    void Awake()
    {
        GetComponent<Image>().sprite = character.image;
        GetComponent<Image>().preserveAspect = true;
        parentObject = transform.parent.parent.parent.parent;
        preMana = FindObjectOfType<PreviewManager>();

        characterIconImage.sprite = character.characteristicIcon;
        costText.text = character.cost.ToString(); ;
    }

    /// <summary>
    /// ドラッグ開始時
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        CreateDragObject();
        dragObject.transform.position = GetMousePosition();
    }

    /// <summary>
    /// ドラッグ中
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnDrag(PointerEventData pointerEventData)
    {
        dragObject.transform.position = GetMousePosition();
    }

    /// <summary>
    /// ドラッグ終了時
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnEndDrag(PointerEventData pointerEventData)
    {
        gameObject.GetComponent<Image>().color = Vector4.one;
        Destroy(dragObject);
    }

    /// <summary>
    /// クリック時 Previwe用
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerEnter == null) { return; }
        preMana.DisplayPreview();
        var cardInfo = pointerEventData.pointerEnter.GetComponent<CardPoolObject>();
        preMana.nameText.text = cardInfo.character.characterName;
        preMana.costText.text = cardInfo.character.cost.ToString();
        preMana.characterImage.sprite = cardInfo.character.image;
        preMana.explanationText.text = preMana.data.GetExplanation((int)cardInfo.character.myCardType);
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

    /// <summary>
    /// DragしたObjectの作成
    /// </summary>
    private void CreateDragObject()
    {
        Vector3 mousePos = Input.mousePosition;
        dragObject = new GameObject("DragObject");
        dragObject.transform.SetParent(parentObject);
        dragObject.transform.SetAsLastSibling();
        dragObject.transform.localPosition = mousePos;
        dragObject.transform.localScale = Vector3.one;

        CanvasGroup canvasGroup = dragObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        Image dragImage = dragObject.AddComponent<Image>();
        Image souceImage = GetComponent<Image>();

        dragImage.sprite = souceImage.sprite;
        dragImage.rectTransform.sizeDelta = souceImage.rectTransform.sizeDelta;
        dragImage.color = souceImage.color;
        dragImage.material = souceImage.material;
        dragImage.preserveAspect = true;

        gameObject.GetComponent<Image>().color = Vector4.one * 0.6f;
    }


}
