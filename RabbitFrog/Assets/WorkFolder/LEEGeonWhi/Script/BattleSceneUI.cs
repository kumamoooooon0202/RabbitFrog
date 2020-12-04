using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneUI : MonoBehaviour
{
    [SerializeField]
    private Effect_Sketch effect_Sketch;

    void Start()
    {
        
    }

    void Update()
    {
        if(effect_Sketch.Scene_changing)
        {
            GetComponent<GraphicRaycaster>().enabled = false;
        }

        else
            GetComponent<GraphicRaycaster>().enabled = true;
        {
        }
    }
}
