using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handBackGround : MonoBehaviour
{
    [SerializeField] private BattleController battleController;
    private Image image;

    [SerializeField]
    private Image HandBG;
    [SerializeField]
    Sprite[] chara_ = new Sprite[2];

    public int cost = 0;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = battleController.SummonGageVal / cost;

        //2021-01-26 イゴンヒ
        if(battleController.SummonGageVal >= cost)
        {
            HandBG.sprite = chara_[1];
        }

        else
        {
            HandBG.sprite = chara_[0];
        }
    }
}
