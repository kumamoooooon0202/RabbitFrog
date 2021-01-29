using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieCharacterAnimation : MonoBehaviour
{
    public GameObject[] rabbit;
    public GameObject[] frog;

    public GameObject rabbits;
    public GameObject frogs;

    public bool isCharacter = false;

    void Start()
    {
        frogs.SetActive(false);
    }

    public GameObject[] GetChara
    {
        get
        {
            if (isCharacter)
            {
                return frog;
            }
            else
            {
                return rabbit;
            }
        }
    }

    public GameObject GetCharas
    {
        get
        {
            if (isCharacter)
            {
                return frogs;
            }
            else
            {
                return rabbits;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetCharas.SetActive(false);
            isCharacter = !isCharacter;
            GetCharas.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (var chara in GetChara)
            {
                chara.GetComponent<Animator>().SetBool("IsMove", true);
                chara.GetComponent<Animator>().SetTrigger("AttackTrigger");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (var chara in GetChara)
            {
                chara.GetComponent<Animator>().SetBool("IsMove", true);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (var chara in GetChara)
            {
                chara.GetComponent<Animator>().SetTrigger("IsDeath");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

        }
        
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

        }
        
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {

        }
        
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {

        }
    }

    public void CharasDisplaySwitching()
    {
        GetCharas.SetActive(false);
        isCharacter = !isCharacter;
        GetCharas.SetActive(true);
    }

}
