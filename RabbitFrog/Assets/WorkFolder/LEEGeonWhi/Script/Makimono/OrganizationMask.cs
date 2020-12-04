using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrganizationMask : MonoBehaviour
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
        //if (image.fillAmount <= 1 && start_anime)
        //{
        //    image.fillAmount += Time.deltaTime;
        //}

        //if (image.fillAmount >= 0 && !start_anime)
        //{
        //    image.fillAmount -= Time.deltaTime;
        //}


    }

    //public void Open_animation()
    //{
    //    //start_anime = !start_anime;
    //    start_anime = true;
    //}

    /// <summary>
    /// アニメーション起動
    /// </summary>
    /// <param name="Close_Target">閉じるCanvas Target</param>
    /// <returns></returns>
    public IEnumerator Close_Open(StageSelectMask Close_Target)
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

        while (image.fillAmount <= 1)
        {
            if (image.fillAmount >= 1) break;
            yield return new WaitForSeconds(Time.deltaTime);
            image.fillAmount += Time.deltaTime;
        }
        OptionController.is_runing = false;
    }

    public IEnumerator Open()
    {
        OptionController.is_runing = true;
        while (image.fillAmount <= 1)
        {
            if (image.fillAmount >= 1) break;
            yield return new WaitForSeconds(Time.deltaTime);
            image.fillAmount += Time.deltaTime;
        }
        OptionController.is_runing = false;
    }

    public IEnumerator Close()
    {
        OptionController.is_runing = true;

        while (image.fillAmount >= 0)
        {
            if (image.fillAmount <= 0) break;
            yield return new WaitForSeconds(Time.deltaTime);
            image.fillAmount -= Time.deltaTime;
        }
        OptionController.is_runing = false;
    }
}
