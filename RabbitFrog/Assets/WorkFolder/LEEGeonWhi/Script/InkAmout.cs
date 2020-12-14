using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkAmout : MonoBehaviour
{
    public static Image image;
    private static RectTransform rectTransform;

    //Vector2 InkClose = new Vector2(30, 70);
    //Vector2 InkOpen  = new Vector2(30, 20);

    static Vector2 InkClose = new Vector2(30, 20);
    //Inspectorから修正
    static Vector2 InkOpen;

    void Start()
    {

        image = GetComponent<Image>();
        image.fillAmount = 1.0f;

        rectTransform = GetComponent<RectTransform>();
        //rectTransform.transform;

        InkOpen.x = rectTransform.rect.width;
        InkOpen.y = rectTransform.rect.height;

    }

    /// <summary>
    /// インクの残量が減らす
    /// </summary>
    static public void decrease_Gauge(float amount)
    {
        if (image.fillAmount <= 0) return;
        image.fillAmount -= amount;
    }

    /// <summary>
    /// インクの残量が上がる
    /// </summary>
    /// <param name="amount"></param>
    static public void increase_Gauge(float amount)
    {
        if (image.fillAmount >= 1) return;
        image.fillAmount += amount;
    }

    /// <summary>
    /// インクの残り残量をチェック
    /// </summary>
    /// <returns></returns>
    static public bool inkChack()
    {
        if (image.fillAmount <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// インクモード時ゲージ表示（拡大）
    /// </summary>
    static public void ImageOpen()
    {

        rectTransform.sizeDelta = InkOpen;
    }

    /// <summary>
    /// インクモード時ゲージ表示（縮小）
    /// </summary>
    static public void ImageClose()
    {
        rectTransform.sizeDelta = InkClose;
    }
}
