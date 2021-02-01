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
    bool colorChanged = false;

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
        if(Parent_script.hp > Max_HP / 3 && !colorChanged)
        {
            ////for (int i = 0; i < sp.Length; i++)
            //for (int i = 0; i < sp2.Count; i++)
            //{
            //    //sp2[i].enabled = false;
            //    //sp[i].color = new Color(0.5803922f, 0.3805103f, 0.3805103f, 0.0f);
            //    sp2[i].color = new Color(0.5803922f, 0.3805103f, 0.3805103f, 0.0f);
            //}
            //colorChanged = true;

            StartCoroutine(Fade());
            colorChanged = true;
        }
    }


    IEnumerator Fade()
    {
        for (int i = 0; i < sp2.Count; i++)
        {
                sp2[i].enabled = false;
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < sp2.Count; i++)
        {
            sp2[i].enabled = true;
        }
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Fade());
    }

}
