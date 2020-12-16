using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class LineController : MonoBehaviour
{

    static public int MaxLine = 0;

    static public Vector2 startPos;
    static public Vector2 endPos;
    private Vector2 tempPos;
    private Vector2 circle_center;

    static public List<Vector2> Points = new List<Vector2>(); //マウス移動経路　
    private List<Vector2> Points_X = new List<Vector2>(); //Pointsをposition.xを基準にしてlist整列   
    private List<Vector2> Points_Y = new List<Vector2>(); //Pointsをposition.Yを基準にしてlist整列

    [SerializeField]
    private GameObject Drawing_obj;
    [SerializeField]
    private GameObject line_prefab;
    [SerializeField]
    private GameObject Wallparent;

    private GameObject mouce_obj;

    [SerializeField]
    private GameObject circle;

    [SerializeField]
    private GameObject Triangle;

    [SerializeField]
    private List<Vector3> Points_normal = new List<Vector3>();
    [SerializeField]
    private float MaxYPos = 0;

    private LineRenderer lineRenderer; //生成したprefabのLinRenderer
    private LineRenderer Pointer_linRenderer;
    private LineRenderer Tri_linRenderer;

    static public bool is_inkMode = false; //インクモード

    [SerializeField]
    private Vector2 start_screenLimit = new Vector2(-5.0f, -2.5f); //線を書けるScreen範囲1 
    [SerializeField]
    private Vector2 end_screenLimit = new Vector2(5.0f, 5.0f);   //線を書けるScreen範囲2

    private bool is_Drawing = false; //今、線を書いてる中

    //=======================================
    TriangleClass triangleClass = new TriangleClass();
    CircleClass circleClass = new CircleClass();
    LineClass lineClass = new LineClass();

    [SerializeField] RectTransform Set_rt;
    Vector3 rt_temp;

    public InkModeAnim isInkModeAnim;

    void Start()
    {
        rt_temp = Set_rt.localScale;
        MaxLine = 0;
        is_inkMode = false;
        //StartCoroutine(GaugeRecovery());
    }

    void Update()
    {
        //InkAmout.increase_Gauge(Time.deltaTime / 5);
        DrawLine();
    }

    private void FixedUpdate()
    {
        InkAmout.increase_Gauge(0.0005f);
    }

    public void OnIsDraw()
    {
        is_inkMode = !is_inkMode;
        
    }
    public void Down()
    {
        rt_temp.y = 1.8f;   // サイズが小さくなる
        Set_rt.localScale = rt_temp;
    }

    public void Up()
    {
        rt_temp.y = 2f;     // サイズが大きくなる
        Set_rt.localScale = rt_temp;
        isInkModeAnim.InkAnim();
    }

    private void DrawLine()
    {
        if (is_inkMode == false || InkAmout.inkChack() == false) return;

        //マウスをクリックして線が始まる地点を決める
        if (Input.GetMouseButtonDown(0))
        {


            //SceenPositionをWorldPositionへ変換
            startPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                  Input.mousePosition.y,
                                                                 -Camera.main.transform.position.z));


            if (!draw_able()) return;
            is_Drawing = true;

            mouce_obj = Instantiate(Drawing_obj, startPos, Quaternion.identity);


            //list初期化
            //----------------------------------------
            Points.RemoveRange(0, Points.Count);
            Points_X.RemoveRange(0, Points_X.Count);
            Points_Y.RemoveRange(0, Points_Y.Count);
            Points_normal.RemoveRange(0, Points_normal.Count);
        }

        //マウスをドラッグして線が終わる地点を決める
        if (Input.GetMouseButton(0) && is_Drawing)
        {

            endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                Input.mousePosition.y,
                                                                -Camera.main.transform.position.z));

            if (!draw_able()) return;
            //マウスを動かないとlistの追加をしない
            if (Vector2.Distance(tempPos, endPos) < 0.5f) return;

            Points.Add(endPos);
            Points_X.Add(endPos);
            Points_Y.Add(endPos);

            Drawing_obj.transform.position = endPos;
            mouce_obj.transform.position = endPos;
            tempPos = endPos;

        }

        //線を書きを終了する。
        if (Input.GetMouseButtonUp(0) && is_Drawing)
        {
            Destroy(mouce_obj);

            is_Drawing = false;

            //--------------------
            Points_X.Sort((s1, s2) => s1.x.CompareTo(s2.x)); //position.xを基準にして配列整列
            Points_Y.Sort((s1, s2) => s1.y.CompareTo(s2.y)); //position.ｙを基準にして配列整列

            Chack_Point_Normal();

            //直線生成
            if (lineClass.Chack_Line() && Points.Count < 10)
            {
                if (MaxLine > 3 || InkAmout.image.fillAmount < 0.3f) return;
                Instantiate(Wallparent, startPos, Quaternion.identity);
                //InkAmout.decrease_Gauge(0.3f);
                MaxLine++;
            }

            //三角形を生成
            else if (triangleClass.Chack_Traiangle(startPos, Points_Y[Points_Y.Count - 1], endPos)
                    && Points.Count < 10)
            {
                if (InkAmout.image.fillAmount < 0.3f) return;
                float Tri_Height = Mathf.Abs((Points_Y[Points_Y.Count - 1].y - startPos.y));
                float Tri_Width = Mathf.Abs((endPos.x - startPos.x));
                float Tri_area = Tri_Height * Tri_Width / 2;

                Vector3 Center = Vector3.Lerp(startPos, endPos, 0.5f);
                var Tri_obj = Instantiate(Triangle, new Vector2(0, 0), Quaternion.identity);
                //20-12-04 修正
                Tri_obj.GetComponent<Triangle>().HP = Tri_area * 0.1f;

                Tri_linRenderer = Tri_obj.GetComponent<LineRenderer>();
                Tri_linRenderer.SetPosition(0, Center);
                Center.y += Tri_Height;
                Tri_linRenderer.SetPosition(1, Center);
                //20-12-04 修正

                InkAmout.decrease_Gauge(Tri_area * 0.1f);
            }

            // draw circle
            else if (circleClass.Chack_Circle(Points_normal))
            {

                if (Points.Count < 8 || Points.Count > 20 || InkAmout.image.fillAmount < 0.5f) return;
                circle_center = new Vector2(
                                             (Points_Y[0].x + Points_Y[Points_Y.Count - 1].x) / 2,
                                             (Points_X[0].y + Points_X[Points_X.Count - 1].y) / 2
                                            );
                Instantiate(circle, circle_center, Quaternion.identity);
                //InkAmout.decrease_Gauge(0.5f);
            }


        }

    }

    /// <summary>
    /// 線が書けるエリアを判定
    /// </summary>
    /// <param name="mousePosition"></param>
    /// <returns></returns>
    private bool draw_able()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (pos.x > start_screenLimit.x && pos.x < end_screenLimit.x &&
            pos.y > start_screenLimit.y && pos.y < end_screenLimit.y)
        {
            return true;
        }
        //mousePosition.y > 100.0f && mousePosition.y < 300.0f;
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Chack_Point_Normal()
    {
        float temp = 0;
        for (int i = 0; i < Points.Count - 1; i++)
        {
            Points_normal.Add(Vector3.Normalize(Points[i + 1] - Points[i]));
            if (Points_normal[i].y < 0 && temp > 0)
            {
                MaxYPos = Points[i].y;
                //return;
            }
            temp = Points_normal[i].y;
        }
    }

    //2秒で－1回復　ゲージ
    IEnumerator GaugeRecovery()
    {
        InkAmout.increase_Gauge(0.1f);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(GaugeRecovery());
    }
    //=============================================================================================
    /// <summary>
    /// Debug用(ゲーム実行には影響はなし)
    /// </summary>
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position

        Vector2 sPos = new Vector3(start_screenLimit.x, start_screenLimit.y, -Camera.main.transform.position.z);
        Vector2 ePos = new Vector3(end_screenLimit.x, end_screenLimit.y, -Camera.main.transform.position.z);

        Vector2 Top_Left = new Vector2(sPos.x, ePos.y);
        Vector2 Top_Right = ePos;
        Vector2 Bottom_Left = sPos;
        Vector2 Bottom_Right = new Vector2(ePos.x, sPos.y);

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawLine(Top_Left, Top_Right);
        Gizmos.DrawLine(Top_Right, Bottom_Right);
        Gizmos.DrawLine(Bottom_Right, Bottom_Left);
        Gizmos.DrawLine(Bottom_Left, Top_Left);

        //Gizmos.DrawCube(transform.position, Pos);
        //Gizmos.DrawCube(transform.position, new Vector3(10, 10, 1));
    }

}