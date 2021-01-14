using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    //StageClear[0] = stage1
    //StageClear[1] = stage2
    //StageClear[2] = stage3
    public static bool[] StageClear = new bool[5] { true, false, false, false, false};

    public static void SaveReset()
    {
        StageClear[0] = true;
        for(int i = 1; i < StageClear.Length; i++)
        {
            StageClear[i] = false;
        }
    }
}
