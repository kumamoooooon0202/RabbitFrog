using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WallParent : MonoBehaviour
{
    //[HideInInspector]
    public float HP = 0;

    //===========================
    private Vector2 _startPos;
    private Vector2 _endPos;
    private Vector3 Dir;
    private Vector2 offset = new Vector2(0, 1.0f);
    private Vector2 temp;

    private GameObject obj;

    private float LineLength = 0;

    [SerializeField]
    private GameObject Wall_prefab;

    void Awake()
    {
        _startPos = LineController.startPos;
        _endPos = LineController.endPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, 5.0f);

        LineLength = Mathf.Abs(_startPos.y - _endPos.x);
        HP = 1 + 0.2f * (LineLength - 1);

        //20-12-04　イゴンヒ
        InkAmout.decrease_Gauge(LineLength * 0.1f);

        Dir = Vector3.Normalize(_startPos - _endPos);
        for (int i = 0; i < LineController.Points.Count - 1; i++)
        {
            if (Dir.y < 0)
            {
                _startPos.x -= 0.1f;
                _startPos.y += 0.2f;
                temp = offset;
            }

            else if (Dir.y > 0)
            {
                _startPos.x += 0.1f;
                _startPos.y -= 0.2f;
                temp = -offset;
            }

            obj = Instantiate(Wall_prefab, _startPos + temp, Quaternion.identity);
            obj.transform.parent = gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
            LineController.MaxLine--;
            //InkAmout.increase_Gauge(0.1f);
        }
    }
}
