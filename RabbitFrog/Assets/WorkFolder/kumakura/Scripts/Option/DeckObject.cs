using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckObject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Sprite uiMask;
    public Image iconImage;
    public Sprite nowSprite;
    public CardPoolObject cardPoolObject;
    public PreviewManager preMana;

    public Image characterIconImage = null;
    public Text costText = null;

    //2010225　イゴンヒ
    void Awake()
    {
        nowSprite = uiMask;
        preMana = FindObjectOfType<PreviewManager>();
    }

    void Start()
    {
        
    }

    /// <summary>
    /// マウスカーソルがオブジェクトに乗ったとき
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null || pointerEventData.pointerDrag.tag != "CardPool") return;
        Debug.Log(pointerEventData.pointerDrag.tag);
        Image dropImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = dropImage.sprite;
        iconImage.color = Vector4.one;
    }

    /// <summary>
    /// マウスカーソルがオブジェクトから離れたとき
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;
        iconImage.sprite = nowSprite;
        if (nowSprite == null) { iconImage.color = Vector4.one; }
        else { iconImage.color = Vector4.one; }
    }

    /// <summary>
    /// ドラッグをやめたとき
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnDrop(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag.tag != "CardPool") return;
        Image dropImage = pointerEventData.pointerDrag.GetComponent<Image>();
        cardPoolObject = pointerEventData.pointerDrag.GetComponent<CardPoolObject>();
        cardPoolObject.character.myCardType = pointerEventData.pointerDrag.GetComponent<CardPoolObject>().character.myCardType;
        iconImage.sprite = dropImage.sprite;
        nowSprite = dropImage.sprite;
        iconImage.color = Vector4.one;

        characterIconImage.sprite = cardPoolObject.character.characteristicIcon;
        costText.text = cardPoolObject.character.cost.ToString(); ;


        //==============
        int index  = this.gameObject.name.IndexOf("(") + 1;
        Debug.Log(this.gameObject.name[index]);
        //==============
    }

    /// <summary>
    /// クリック時 Previwe用
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        var cardInfo = pointerEventData.pointerEnter.GetComponent<DeckObject>().cardPoolObject;
        if (cardInfo == null) { return; }
        preMana.DisplayPreview();
        preMana.nameText.text = cardInfo.character.characterName;
        preMana.costText.text = cardInfo.character.cost.ToString();
        preMana.characterImage.sprite = cardInfo.character.image;
        preMana.iconImage.sprite = cardInfo.character.characteristicIcon;
        preMana.explanationText.text = preMana.data.GetExplanation((int)cardInfo.character.myCardType);
    }
}
