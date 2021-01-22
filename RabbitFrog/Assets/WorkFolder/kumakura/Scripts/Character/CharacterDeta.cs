using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterDeta
{
    public string name = "";
    public int hp;
    public int maxHp;
    public int power;
    public string[] explanation = new string[12];


    //No,  名前      英語名          特徴
    //01,  歩兵      Infantry	　
    //02,  歩兵小隊  InfantryPlatoon 
    //03,  騎兵      Cavalry
    //04,  騎士      Knight
    //05,  侍        Samurai
    //06,  弓術隊    ArcheryCorps
    //07,  忍者      Ninja           隠密
    //08,  騎将      CavalryGeneral
    //09,  重騎士    HeavyKnight     鉄壁
    //10,  怪物      Monster 爆発
    //11,  死霊術師  Necromancer
    //12,  雷神      ThunderGod      感電
    //13,  死霊      Ghost           爆発

    // とりあえずでここに書いておくンゴ
    public void SetExplanation()
    {
        explanation[0] = "力も体力も少ないが\nコストも低く気軽に\n召喚することができる";
        explanation[1] = "一度に歩兵を複数体召喚できる為\n防御が硬い敵に対して効果的";
        explanation[2] = "速度も速く敵のタンク\nを無視して大将に攻撃\nしに向かう";
        explanation[3] = "敵の攻撃力に関係なく１ダメージしか\n食らわないため敵の強力な攻撃にも耐えられる";
        explanation[4] = "攻撃力も体力もバランスのいいオールラウンダー";
        explanation[5] = "敵を遠距離から一方的に攻撃することができる。\n体力は低いが複数同時に召喚できる";
        explanation[6] = "隠密が得意で敵の遠距離職に見つからない。\n敵大将にも攻撃するまで気づかれない";
        explanation[7] = "騎兵の上位職で\n攻撃力も体力も高い";
        explanation[8] = "騎士の上位職で\n攻撃力も耐久力も高い";
        explanation[9] = "攻撃力も体力も高く、\n複数体を攻撃できるが、\nその分コストも高い";
        explanation[10] = "死霊を召喚しながら敵に向かって攻撃していく。\n一定時間ごとに召喚するため時間が経つほど有利";
        explanation[11] = "広範囲の敵を攻撃でき\n確率で感電させ足止めすることができる";
    }

    public string GetExplanation(int id)
    {
        return explanation[id];
    }
}
