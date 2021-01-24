using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterBase : MonoBehaviour
{
    [Header("キャラクターの名前")] public string characterName;         // キャラクターの名前
    [Header("キャラクター画像")] public Sprite image;          // キャラクター画像
    [Header("アイコン")] public Sprite characteristicIcon; 　　// キャラクターアイコン
    [Header("体力")] public int hp;                            // 体力
    [Header("攻撃力")] public int power;                       // 攻撃力
    [Header("特徴")] public characteristic myCharacteristic;   // 特徴
    [Header("自身のカード名")] public CardType myCardType;     // 種類
    [Header("表示するHPのText")] public Text hpText;           // HP用のText
    [Header("アイコン用のImage")] public Image iconImage;      // Icon用のImage

    public enum CardType
    {
        none = -1,
        infantry,
        infantryPlatoon,
        cavalry,
        knight,
        samurai,
        archeryCorps,
        ninja,
        cavalryGeneral,
        heavyKnight,
        monster,
        necromancer,
        thunderGod,
        ghost,
    }

    public enum characteristic // 特徴
    {
        none,               // 無し
        quickness,          // 俊足
        ironWall,           // 鉄壁
        covert,             // 隠密
        explosion,          // 爆発
        electricShock,      // 感電
    }

    // 動けるかどうかのFlag(感電用)
    public bool IsMove { get; set; } = true;

    public bool IsDeath { get; set; } = false;

    public abstract void Death();
    public abstract void Attack();

}
