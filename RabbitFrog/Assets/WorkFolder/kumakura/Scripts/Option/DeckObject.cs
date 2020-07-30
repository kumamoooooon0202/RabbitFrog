﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckObject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image iconImage;
    private Sprite nowSprite;
    public CardPoolObject cardPoolObject;


    void Start()
    {
        nowSprite = null;
        //cardPoolObject = FindObjectOfType<CardPoolObject>();
        //cardPoolObject.myCardType = CardPoolObject.CardType.none;
        //Debug.Log(cardPoolObject);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;
        Image dropImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = dropImage.sprite;
        iconImage.color = Vector4.one;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;
        iconImage.sprite = nowSprite;
        if (nowSprite == null) { iconImage.color = Vector4.one; }
        else { iconImage.color = Vector4.one; }
    }

    public void OnDrop(PointerEventData pointerEventData)
    {
        Image dropImage = pointerEventData.pointerDrag.GetComponent<Image>();
        cardPoolObject = pointerEventData.pointerDrag.GetComponent<CardPoolObject>();
        cardPoolObject.myCardType = pointerEventData.pointerDrag.GetComponent<CardPoolObject>().myCardType;
        //Debug.Log(pointerEventData.pointerDrag.GetComponent<CardPoolObject>().myCardType);
        Debug.Log(cardPoolObject.myCardType);
        iconImage.sprite = dropImage.sprite;
        nowSprite = dropImage.sprite;
        iconImage.color = Vector4.one;
    }
}
