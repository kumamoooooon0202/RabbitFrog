using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MaskControl : MonoBehaviour
{
    public Image image;
    public bool is_status = false;
    //public bool start_anime = false;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Close_Targetを閉じて編成画面を開く処理
    /// </summary>
    /// <param name="Close_Target">閉じるCanvas Target</param>
    /// <returns></returns>
    public IEnumerator Close_Open(MaskControl Close_Target, RectTransform ObjTarget1, RectTransform ObjTarget2)
    {
        OptionController.is_runing = true;
        //StartCoroutine(Close_Target.Close());
        //yield return StartCoroutine(Close_Target.Close());

        while (Close_Target.image.fillAmount >= 0)
        {
            if (Close_Target.image.fillAmount <= 0) break;
            yield return new WaitForSeconds(Time.deltaTime);
            Close_Target.image.fillAmount -= Time.deltaTime;
        }
        ObjTarget2.sizeDelta = new Vector2(75, 450);
        ObjTarget1.sizeDelta = new Vector2(75, 600);

        while (image.fillAmount <= 1)
        {
            if (image.fillAmount >= 1) break;
            yield return new WaitForSeconds(Time.deltaTime);
            image.fillAmount += Time.deltaTime;
        }
        OptionController.is_runing = false;
    }

    /// <summary>
    /// 編成画面を開く処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator Open(RectTransform ObjTarget)
    {
        OptionController.is_runing = true;
        ObjTarget.sizeDelta = new Vector2(75, 600);
        while (image.fillAmount <= 1)
        {
            if (image.fillAmount >= 1) break;
            yield return new WaitForSeconds(Time.deltaTime);
            image.fillAmount += Time.deltaTime;
        }
        OptionController.is_runing = false;
    }

    /// <summary>
    /// 編成画面を閉じる処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator Close(RectTransform ObjTarget)
    {
        OptionController.is_runing = true;

        while (image.fillAmount >= 0)
        {
            if (image.fillAmount <= 0) break;
            yield return new WaitForSeconds(Time.deltaTime);
            image.fillAmount -= Time.deltaTime;
        }
        ObjTarget.sizeDelta = new Vector2(75, 450);
        OptionController.is_runing = false;
    }
}