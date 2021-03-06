﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField]
    SpriteRenderer[] sp;

    // Start is called before the first frame update
    void Start()
    {
        sp = new SpriteRenderer[transform.childCount - 1];
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            sp[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            for(int i = 0; i < sp.Length; i++)
            {
                sp[i].color = new Color(1, 0, 0, 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < sp.Length; i++)
            {
                sp[i].color = new Color(1, 1, 1, 1);
            }
        }
    }
}
