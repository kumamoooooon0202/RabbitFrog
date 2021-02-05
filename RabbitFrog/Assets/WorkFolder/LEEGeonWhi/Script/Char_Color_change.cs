using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Color_change : MonoBehaviour
{
    [SerializeField]
    CharacterBase Parent_script;

    //[SerializeField]
    //SpriteRenderer[] sp;

    private int Max_HP;
    private float BlinkSpeed = 0.5f;
    bool colorChanged = false;

    Color temp;

    [SerializeField]
    List<SpriteRenderer> sp2 = new List<SpriteRenderer>();

    // Start is called before the first frame update
    private void Awake()
    {
        Parent_script = transform.parent.gameObject.GetComponent<CharacterBase>();
        //sp = new SpriteRenderer[transform.GetChildCount() - 1];
        for (int i = 0; i < transform.GetChildCount() - 1; i++)
        {
            //sp[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();

            SpriteRenderer thisSP = transform.GetChild(i).GetComponent<SpriteRenderer>();
            if (thisSP != null)
                sp2.Add(thisSP);
        }
        Max_HP = Parent_script.hp;

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Parent_script.hp < Max_HP * 0.05f)
        {
            ColorChange(0, 0, 0);
        }

        else if (Parent_script.hp < Max_HP * 0.20f)
        {
            ColorChange(0.176f, 0.176f, 0.176f);
        }

        else if (Parent_script.hp < Max_HP * 0.35f)
        {
            ColorChange(0.525f, 0.525f, 0.525f);
        }

        else if (Parent_script.hp < Max_HP * 0.5f)
        {
            ColorChange(0.694f, 0.694f, 0.694f);
        }
    }

    void ColorChange(float R, float G, float B)
    {
        if (temp == new Color(R, G, B, 1.0f)) return;

            for (int i = 0; i < sp2.Count; i++)
            {
                sp2[i].color = new Color(R, G, B, 1.0f);
            }

        temp = new Color(R, G, B, 1.0f);
    }

    //IEnumerator Blinke()
    //{
    //    for (int i = 0; i < sp2.Count; i++)
    //    {
    //            sp2[i].enabled = false;
    //    }
    //    yield return new WaitForSeconds(0.1f);
    //    for (int i = 0; i < sp2.Count; i++)
    //    {
    //        sp2[i].enabled = true;
    //    }
    //    yield return new WaitForSeconds(0.1f);

    //    StartCoroutine(Blinke());
    //}

}
