using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    private BoxCollider2D Box_col;

    public float HP;

    private float DeleyTime = 3.0f;
    private float TempTime;
    float HP_Magnification;

    private void Awake()
    {
        HP_Magnification = LineController.HP_Magnification;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        //Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    { 
        if(DeleyTime < 0)
        {
            Destroy(gameObject);
            InkAmout.increase_Gauge(HP * HP_Magnification);
        }
        DeleyTime -= Time.deltaTime;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<Enemy>().hp -= 3;
        }

    }
    void Init()
    {
        Box_col = gameObject.AddComponent<BoxCollider2D>();
        Box_col.isTrigger = true;
    }
}
