using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField]
    SpriteRenderer[] sp;

    [SerializeField]
    SpriteRenderer test;
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
        test.color = new Color(1, 0, 0, 0);

    }
}
