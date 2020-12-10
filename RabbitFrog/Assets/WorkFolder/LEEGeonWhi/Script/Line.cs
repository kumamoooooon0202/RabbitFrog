using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Line : MonoBehaviour
{
    public float lineLength;
    public float HP;

    private BoxCollider2D Box_col;
    private bool Check_Overlap;
    

    void Start()
    {
        Init();
        StartCoroutine(Change_Overlap());
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            //InkAmout.increase_Gauge(0.1f);
            Destroy(gameObject);
        }
    }

    void Init()
    {
        Box_col = gameObject.AddComponent<BoxCollider2D>();
        Box_col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" && Check_Overlap == false)
        {
            HP -= 0.5f;
            //col.GetComponent<Enemy>().IsMove = false;
            //Enemy_obj.Add(col.gameObject);
            col.GetComponent<Enemy>().transform.position += new Vector3(-Time.deltaTime * 3, 0, 0); //壁に衝突するMotion
        }
    }

    /// <summary>
    /// 判定用 function
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerStay2D(Collider2D col)
    {
        //if (col.tag == "Enemy" && Check_Overlap == true)
        //{
        //    InkAmout.increase_Gauge(0.1f);
        //    Destroy(gameObject);
        //}

        if (col.tag == "Enemy")
        {
            col.GetComponent<Enemy>().transform.position += new Vector3(-Time.deltaTime * 3, 0, 0); //壁に衝突するMotion
        }
    }

    ///// <summary>
    ///// 判定用 function
    ///// </summary>
    ///// <param name="col"></param>
    //void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.tag == "Enemy")
    //    {
    //        col.GetComponent<Enemy>().IsMove = true;
    //        Debug.Log("判定なし test");
    //    }
    //}


    IEnumerator Change_Overlap()
    {
        Check_Overlap = true;
        yield return new WaitForSeconds(0.05f);
        Check_Overlap = false;
    }
}
