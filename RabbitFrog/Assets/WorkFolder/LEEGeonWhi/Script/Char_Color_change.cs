using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Color_change : MonoBehaviour
{
    [SerializeField]
    Character Parent_script;

    [SerializeField]
    SpriteRenderer[] sp;

    private int Max_HP;
    bool colorChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        Parent_script = transform.parent.gameObject.GetComponent<Character>();
        sp = new SpriteRenderer[transform.GetChildCount() - 1];
        for (int i = 0; i < transform.GetChildCount() - 1; i++)
        {
            sp[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
        Max_HP = Parent_script.hp;

    }

    // Update is called once per frame
    void Update()
    {
        if(Parent_script.hp < Max_HP / 3 && !colorChanged)
        {
            for (int i = 0; i < sp.Length; i++)
            {
                Color color = new Color(0.5803922f, 0.3805103f, 0.3805103f, 0.0f);
                sp[i].color = color;
            }
            colorChanged = true;
        }
    }
}
