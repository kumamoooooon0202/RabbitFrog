using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AutoDeckArray
{
    [Header("自動セットしたいカード")]
    public CardPoolObject[] autoSet = new CardPoolObject[8];
}

public class AutoDeckSet : MonoBehaviour
{
    private int deckNumber = 0;
    private int countUpDeckNumber
    { 
        get
        {
            deckNumber++;
            if (autoDeckArray.Length <= deckNumber) { deckNumber = 0; }
            return deckNumber;
        }
    }
    [SerializeField] private Image deckImage = null;
    [SerializeField] private Sprite[] deckSetImages = new Sprite[4];
    [SerializeField] private GameObject[] decks = new GameObject[8];
    //[SerializeField] private Image[] deckImage = new Image[8];
    //[SerializeField] private DeckObject[] deckObjects = new DeckObject[8];
    //[SerializeField, Header("自動セットしたいカード")] private CardPoolObject[] cardPoolObjects = new CardPoolObject[8];
    [Header("おすすめ編成のパターン")] public AutoDeckArray[] autoDeckArray;

    public void AutoSetDeck()
    {
        for(int i = 0; i < decks.Length; i++)
        {
            //deck[i].GetComponent<DeckObject>().cardPoolObject = cardPoolObjects[i];
            decks[i].GetComponent<DeckObject>().cardPoolObject = autoDeckArray[deckNumber].autoSet[i];
            //deckObjects[i].cardPoolObject = cardPoolObjects[i];
            decks[i].GetComponent<Image>().sprite = decks[i].GetComponent<DeckObject>().cardPoolObject.character.image;
            decks[i].GetComponent<DeckObject>().characterIconImage.sprite = decks[i].GetComponent<DeckObject>().cardPoolObject.character.characteristicIcon;
            decks[i].GetComponent<DeckObject>().costText.text = decks[i].GetComponent<DeckObject>().cardPoolObject.character.cost.ToString();
            //deckImage[i].sprite = deckObjects[i].cardPoolObject.character.image;
            //2020/10/25 bug修正　イゴンヒ
            //deckObjects[i].GetComponent<DeckObject>().nowSprite = deckImage[i].sprite;
            decks[i].GetComponent<DeckObject>().nowSprite = decks[i].GetComponent<Image>().sprite;
        }
        deckImage.sprite = deckSetImages[countUpDeckNumber];
    }
}
