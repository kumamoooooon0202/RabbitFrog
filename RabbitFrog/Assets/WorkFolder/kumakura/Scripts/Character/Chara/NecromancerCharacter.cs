using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerCharacter : Character
{
    [SerializeField] private GameObject ghost;
    [SerializeField, Header("召喚間隔")] private float ghostSummonInterval = 2.0f;
    private float summonTime;

    [SerializeField] private float minRandomPos_x;
    [SerializeField] private float maxRandomPos_x;
    [SerializeField] private float minRandomPos_y;
    [SerializeField] private float maxRandomPos_y;

    private BattleController battlecontroller;
    Vector3 ghostSummonPos;
    private int ghostCount;

    // Start is called before the first frame update
    void Start()
    {
        battlecontroller = FindObjectOfType<BattleController>();
        ghostCount = 0;
        //SummonGhost(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDeath) { return; }
        if (hp <= 0) { Death(); }
        CharacterMove(moveSpeed);
        summonTime += Time.deltaTime;
        //ghostCount追加　イゴンヒ
        if (ghostSummonInterval < summonTime && ghostCount <= 10 && !battlecontroller.isGameSet)
        {
            summonTime = 0;
            SummonGhost(transform.position);
            ghostCount++;
        }
    }

    private void SummonGhost(Vector3 summonPos)
    {
        float randomPos_x = Random.Range(minRandomPos_x, maxRandomPos_x);
        float randomPos_y = Random.Range(minRandomPos_y, maxRandomPos_y);
        summonPos.x += randomPos_x;
        summonPos.y += randomPos_y;
        GameObject Ghosh = Instantiate(ghost, summonPos, Quaternion.identity);
        battlecontroller.characterList.Add(Ghosh);
    }
}
