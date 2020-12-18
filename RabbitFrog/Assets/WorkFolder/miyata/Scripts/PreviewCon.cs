using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PreviewCon : MonoBehaviour, IPointerClickHandler
{
    public PreviewManager preMana;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerEnter == null) { return; }
        int characterId = (int)pointerEventData.pointerEnter.GetComponent<CardPoolObject>().character.myCardType;
        //CharaText.text = Description[characterId];
        preMana.explanationText.text = Description[characterId];
        
    }

    [SerializeField]
    public Text CharaText;
    GameObject CharacterBase;

    //int i = 0;

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
        ghost,
        thunderGod,
    }



    public void Start()
    {
        //string[] Description = { "" };
        string[] Description = new string[11];
        Description[0] = "力も体力も少ないがコストも低く気軽に召喚することができる";
        Description[1] = "一度に歩兵を複数体召喚できるため、防御が堅い敵に対して効果的";
        Description[2] = "速度も速く、敵のタンクを無視して大将に攻撃しに向かう";
        Description[3] = "敵の攻撃力に関係なく１ダメージしか食らわないため敵の強力な攻撃にも耐えられる";
        Description[4] = "攻撃力も体力もバランスのいいオールラウンダー";
        Description[5] = "敵を遠距離から一方的に攻撃することができる体力は低いが複数体同時召喚できる";
        Description[6] = "隠密が得意で敵の遠距離職に見つからない敵大将にも攻撃するまで気づかれない";
        Description[7] = "騎将の上位職で攻撃力も体力も高い";
        Description[8] = "騎士の上位職で攻撃力も体力も高い";
        Description[9] = "攻撃力も体力も高く、複数体を攻撃できるが、その分コストも高い";
        Description[10] = "死霊を召喚しながら敵に向かって攻撃していく一定時間ごとに召喚するため時間がたつほど有利";
        Description[11] = "広範囲の敵を攻撃でき、確率で感電させ足止めすることができる";
    }

    public void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    int i;
    public string[] Description;
    public Dictionary<CardType, string> Charatext = new Dictionary<CardType, string>();

    

    public void OnClick()
    {
        //int Charatext = (int)CardType.infantry;
        //Charatext.Add(CardType.infantry, Description[0]);b
        //CharaText.text = Description[0];

        //Charatext.Add(CardType.infantryPlatoon, Description[1]);
        //CharaText.text = Description[1];

        //Charatext.Add(CardType.cavalry, Description[2]);
        //CharaText.text = Description[2];

        //Charatext.Add(CardType.knight, Description[3]);
        //CharaText.text = Description[3];

        //Charatext.Add(CardType.samurai, Description[4]);
        //CharaText.text = Description[4];

        //Charatext.Add(CardType.archeryCorps, Description[5]);
        //CharaText.text = Description[5];

        //Charatext.Add(CardType.ninja, Description[6]);
        //CharaText.text = Description[6];

        //Charatext.Add(CardType.cavalryGeneral, Description[7]);
        //CharaText.text = Description[7];

        //Charatext.Add(CardType.heavyKnight, Description[8]);
        //CharaText.text = Description[8];

        //Charatext.Add(CardType.monster, Description[9]);
        //CharaText.text = Description[9];

        //Charatext.Add(CardType.necromancer, Description[10]);
        //CharaText.text = Description[10];

        //Charatext.Add(CardType.thunderGod, Description[11]);
        //CharaText.text = Description[11];

    }

    //public void OnClick1()
    //{
    //    Charatext.Add(CardType.infantryPlatoon, Description[1]);
    //    CharaText.text = Description[1];

    //}

    //public void OnClick2()
    //{
    //    Charatext.Add(CardType.cavalry, Description[2]);
    //    CharaText.text = Description[2];
    //}

    //public void OnClick3()
    //{
    //    Charatext.Add(CardType.knight, Description[3]);
    //    CharaText.text = Description[3];
    //}

    //public void OnClick4()
    //{
    //    Charatext.Add(CardType.samurai, Description[4]);
    //    CharaText.text = Description[4];
    //}

    //public void OnClick5()
    //{
    //    Charatext.Add(CardType.archeryCorps, Description[5]);
    //    CharaText.text = Description[5];
    //}

    //public void OnClick6()
    //{
    //    Charatext.Add(CardType.ninja, Description[6]);
    //    CharaText.text = Description[6];
    //}

    //public void OnClick7()
    //{
    //    Charatext.Add(CardType.cavalryGeneral, Description[7]);
    //    CharaText.text = Description[7];
    //}

    //public void OnClick8()
    //{
    //    Charatext.Add(CardType.heavyKnight, Description[8]);
    //    CharaText.text = Description[8];
    //}

    //public void OnClick9()
    //{
    //    Charatext.Add(CardType.monster, Description[9]);
    //    CharaText.text = Description[9];
    //}

    //public void OnClick10()
    //{
    //    Charatext.Add(CardType.necromancer, Description[10]);
    //    CharaText.text = Description[10];
    //}

    //public void OnClick11()
    //{
    //    Charatext.Add(CardType.thunderGod, Description[11]);
    //    CharaText.text = Description[11];
    //}
}
