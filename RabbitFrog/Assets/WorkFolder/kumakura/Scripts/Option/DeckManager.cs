using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    public DeckObject[] getDeckObjects = new DeckObject[8];
    public static DeckObject[] deckObjects = new DeckObject[8];

    void Start()
    {
        //for (int i = 0; i < deckObjects.Length; i++)
        //{
        //    DeckData.deckObjects[i] = getDeckObjects[i];
        //}
        //GetDeckObject();
        //if (DeckData.deckSetFlag)
        //{
        //    Debug.Log("Dataに入れてみるね");
        //    for (int i = 0; i < deckObjects.Length; i++)
        //    {
        //        DeckData.deckObjects[i] = getDeckObjects[i];
        //    }
        //    DeckData.deckSetFlag = false;
        //}
        
        for (int i = 0; i < deckObjects.Length; i++)
        {
            DeckData.Instance.deckObjects[i] = getDeckObjects[i];
            deckObjects[i] = getDeckObjects[i];
            //deckObjects[i] = DeckData.deckObjects[i];
        }

        //イゴンヒ
        //===============
        //if(DeckSave.cardPoolObject[0] != null)
        if (DeckSave.DeckChack() == false)
        {
            for (int i = 0; i < deckObjects.Length; i++)
            {
                deckObjects[i].cardPoolObject = GameObject.Find(DeckSave.cardPoolObject[i]).GetComponent<CardPoolObject>();
                deckObjects[i].GetComponent<Image>().sprite = DeckSave.iconImage[i].sprite;
                deckObjects[i].GetComponent<DeckObject>().nowSprite = DeckSave.iconImage[i].sprite;
                deckObjects[i].characterIcon.sprite = DeckSave.characterIcon[i].sprite;
                deckObjects[i].costText.text = DeckSave.characterCost[i].ToString();
            }
        }
        //===============
        //if (DeckSave.iconImage[0] != null) deckObjects[0].iconImage = DeckSave.iconImage[0];


        //if (DeckData.Instance.deckSetFlag)
        //{
        //    for (int i = 0; i < deckObjects.Length; i++)
        //    {
        //        DeckData.Instance.deckObjects[i] = getDeckObjects[i];
        //        deckObjects[i] = getDeckObjects[i];
        //        //deckObjects[i] = DeckData.deckObjects[i];
        //    }
        //    DeckData.Instance.deckSetFlag = false;
        //}
        //else
        //{
        //    for (int i = 0; i < deckObjects.Length; i++)
        //    {
        //        deckObjects[i] = DeckData.Instance.deckObjects[i];
        //        Debug.Log("参照　" + deckObjects[i]);
        //    }
        //    Debug.Log("参照完了");
        //}
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Setしてみるね");
            //SetDeckObject();
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = 0; i < deckObjects.Length; i++)
            {
                Debug.Log(deckObjects[i].cardPoolObject);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < deckObjects.Length; i++)
            {
                Debug.Log(DeckData.Instance.deckObjects[i]);
            }   
        }



    }

    //ステージを選択すると実行

    public static void SetDeckObject()
    {
        for (int i = 0; i < deckObjects.Length; i++)
        {
            if (deckObjects[i] == null) { return; }
            //DeckData.Instance.deckObjects[i] = deckObjects[i];
            //Debug.Log(DeckData.Instance.deckObjects[i]);
            //Debug.Log(DeckData.Instance.deckObjects[i].cardPoolObject.name);
            //Debug.Log(DeckData.Instance.deckObjects[i].cardPoolObject.character.characterName);
            //Debug.Log(deckObjects[i].iconImage.sprite);

            //int index = deckObjects[i].name.IndexOf("(") + 1;
            //Debug.Log(deckObjects[i].name[index]);
            DeckSave.cardPoolObject[i] = deckObjects[i].cardPoolObject.name;
            DeckSave.iconImage[i] = deckObjects[i].GetComponent<Image>();
            DeckSave.characterIcon[i] = deckObjects[i].characterIcon;
            DeckSave.characterCost[i] = deckObjects[i].cardPoolObject.character.cost;
        }
         
         
        Debug.Log("Set出来たよ");
    }

    public static void GetDeckObject()
    {
        for (int i = 0; i < deckObjects.Length; i++)
        {
            if (DeckData.Instance.deckObjects[i] == null) { Debug.Log("Get出来ませんでした"); return; }
            deckObjects[i] = DeckData.Instance.deckObjects[i];
            deckObjects[i].iconImage.sprite = DeckData.Instance.deckObjects[i].iconImage.sprite;
            Debug.Log(deckObjects[i].cardPoolObject.character.characterName);
        }
        Debug.Log("Get出来たよ");
    }
}
