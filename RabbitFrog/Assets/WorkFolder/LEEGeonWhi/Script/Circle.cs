using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private float circumference;
    [SerializeField]
    private float HP;
    private float radius;
    private Vector2 circle_center;

    void Start()
    {
        Init();
        InkAmout.decrease_Gauge(HP);
        StartCoroutine(obj_destroy());
    }

    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D col)
    {

        if (col.tag == "Enemy")
        {
            
          Vector3 direction = (transform.position - col.gameObject.transform.position).normalized;
            if (Vector2.Distance(transform.position, col.gameObject.transform.position) < 0.1f) return;
            col.gameObject.transform.position += direction * 0.05f;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Init()
    {
        circumference = 0;
        radius = 0;
        //円の長さを計算する
        for (int i = 0; i < LineController.Points.Count - 1; i++)
        {
            circumference += Vector3.Distance(LineController.Points[i], LineController.Points[i + 1]);
        }
        //半径を計算する
        radius = circumference / 6.28f;

        transform.localScale = new Vector3(radius, radius, 1);

        GetComponent<Circle>().circumference = circumference;
        
        //20-12-04 イゴンヒ
        //GetComponent<Circle>().HP = 1 - (circumference - 1) * 0.2f;
        //GetComponent<Circle>().HP = circumference * 0.02f;
        GetComponent<Circle>().HP = radius * 0.2f;


        if (HP < 0) HP=0.1f;
    }

    IEnumerator obj_destroy()
    {
        while(HP > 0)
        {
            yield return new WaitForSeconds(1.0f);
            HP -= 0.2f;
        }

        //InkAmout.increase_Gauge(0.1f);
        Destroy(gameObject);
    }
}
