using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organization : MonoBehaviour
{
    RectTransform rt;
   
    Vector2 default_Size;
    Vector2 Change_Size;

    Vector2 Open_Pos;
    Vector2 Close_Pos;

    bool is_Click = false;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        default_Size = rt.sizeDelta;
        Change_Size = rt.sizeDelta;
        Change_Size.y = rt.sizeDelta.y + 100;


    }

    public void Open_Close()
    {
        is_Click = !is_Click;
        if(is_Click)
        {
            rt.sizeDelta = Change_Size;
        }

        else
        {
            rt.sizeDelta = default_Size;
        }
    }
    
    public void Open()
    {
        rt.sizeDelta = Change_Size;
    }

    public void Close()
    {
        rt.sizeDelta = default_Size;
    }
}
