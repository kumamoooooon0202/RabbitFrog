﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkMode : MonoBehaviour
{

    [SerializeField]
    Image image;

    RectTransform Set_rt;
    Vector3 rt_temp;

    private bool Lnow = false;

    //[SerializeField]
    //float Width;

    //Material rend;
    // Start is called before the first frame update
    void Start()
    {
        Set_rt = GetComponent<RectTransform>();
        rt_temp = Set_rt.localScale;
        //rend = GetComponent<Renderer>();
        //image.material.shader = Shader.Find("Unlit/OutLine");
    }

    // Update is called once per frame
    void Update()
    {
        // 水墨モードON
        if (LineController.is_inkMode)
        {
            //image.material.SetFloat("_Width", Width);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
            Lnow = true;
            rt_temp.y = 2f;     // ONの状態の時サイズが大きくなる
            Set_rt.localScale = rt_temp;
        }
        // 水墨モードOFF
        if (!LineController.is_inkMode)
        {
            //image.material.SetFloat("_Width", 0);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.6f);
            Lnow = false;
            rt_temp.y = 1.8f;   // OFFの状態の時サイズが小さくなる
            Set_rt.localScale = rt_temp;
        }
        //rend.material.SetFloat("_Shininess", shininess);

    }
}

//mat = GetComponent<Renderer>().material;
//mat.SetColor("_EnergyColor", energyColor);
//mat.SetFloat("_Visibility", visibility);
//mat.SetFloat("_CollisionTime", -99f); /
