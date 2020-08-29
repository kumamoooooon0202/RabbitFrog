﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public GameObject[] createCharacterList = new GameObject[13];
    [SerializeField] private GameObject[] handObjects = new GameObject[4];
    [SerializeField] private GameObject nextHand;
    [SerializeField] private BattleController battleController;
    [SerializeField] private float minRandomPos_x;
    [SerializeField] private float maxRandomPos_x;
    [SerializeField] private float minRandomPos_y;
    [SerializeField] private float maxRandomPos_y;

    [SerializeField]
    public Vector2 start_screenLimit = new Vector2(0, -2.5f); //線を書けるScreen範囲1 
    [SerializeField]
    public Vector2 end_screenLimit = new Vector2(7.0f, 5f);   //線を書けるScreen範囲2

    void Start()
    {
        DeckShuffle();
        SetImage();
    }

    void Update()
    {
        
    }

    private void SetImage()
    {
        int count = 0;
        for (int i = 0; i < handObjects.Length; i++)
        {
            handObjects[count].GetComponent<Image>().sprite = DeckManager.deckObjects[count].iconImage.sprite;
            count++;
        }
        nextHand.GetComponent<Image>().sprite = DeckManager.deckObjects[count].iconImage.sprite;
    }

    /// <summary>
    /// デッキのシャッフル機能
    /// </summary>
    private void DeckShuffle()
    {
        int shuffleVal = 0;
        while (shuffleVal <= 30)
        {
            int randomVal1 = Random.Range(0, DeckManager.deckObjects.Length);
            int randomVal2 = Random.Range(0, DeckManager.deckObjects.Length);
            
            var temp = DeckManager.deckObjects[randomVal1];
            DeckManager.deckObjects[randomVal1] = DeckManager.deckObjects[randomVal2];
            DeckManager.deckObjects[randomVal2] = temp;
            shuffleVal++;
        }
    }

    /// <summary>
    /// キャラクターの召喚
    /// </summary>
    /// <param name="summonPos">召喚場所</param>
    /// <param name="myHandNumber">手札番号</param>
    public void CharacterSummon(Vector3 summonPos, int myHandNumber)
    {
        var deckObj = DeckManager.deckObjects[myHandNumber];
        var cost = deckObj.cardPoolObject.character.cost;
        var myCardType = deckObj.cardPoolObject.character.myCardType;

        if (battleController.SummonGageVal - cost < 0) { return; }
        battleController.SummonGageVal -= cost;

        int summonVal = deckObj.cardPoolObject.character.summonVol; 
        for(int i = 0; i < summonVal; i++)
        {
            if (summonVal > 1)
            {
                float randomPos_x = Random.Range(minRandomPos_x, maxRandomPos_x);
                float randomPos_y = Random.Range(minRandomPos_y, maxRandomPos_y);
                summonPos.x = summonPos.x + randomPos_x;
                summonPos.y = summonPos.y + randomPos_y;
            }
            Instantiate(createCharacterList[(int)myCardType], summonPos, Quaternion.identity);
        }

        // 次手札から補充
        DeckManager.deckObjects[myHandNumber] = DeckManager.deckObjects[4];
        // 画像を次手札から参照
        handObjects[myHandNumber].GetComponent<Image>().sprite = nextHand.GetComponent<Image>().sprite;
        // デッキのリストからランダムに次手札に補充
        int randomHandInt = Random.Range(5, DeckManager.deckObjects.Length);
        DeckManager.deckObjects[4] = DeckManager.deckObjects[randomHandInt];
        // 次手札の画像の設定
        nextHand.GetComponent<Image>().sprite = DeckManager.deckObjects[4].iconImage.sprite;

    }

    //======================================================
    //======================================================
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position

        Vector2 sPos = new Vector3(start_screenLimit.x, start_screenLimit.y, -Camera.main.transform.position.z);

        Vector2 ePos = new Vector3(end_screenLimit.x, end_screenLimit.y, -Camera.main.transform.position.z);

        Vector2 Top_Left = new Vector2(sPos.x, ePos.y);
        Vector2 Top_Right = ePos;
        Vector2 Bottom_Left = sPos;
        Vector2 Bottom_Right = new Vector2(ePos.x, sPos.y);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Top_Left, Top_Right);
        Gizmos.DrawLine(Top_Right, Bottom_Right);
        Gizmos.DrawLine(Bottom_Right, Bottom_Left);
        Gizmos.DrawLine(Bottom_Left, Top_Left);

        //Gizmos.DrawCube(transform.position, Pos);
        //Gizmos.DrawCube(transform.position, new Vector3(10, 10, 1));
    }
}
