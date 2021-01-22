using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkMode : MonoBehaviour
{

    [SerializeField]
    Image image;


    //private bool Lnow = false;

    //[SerializeField]
    //float Width;

    //Material rend;
    // Start is called before the first frame update
    void Start()
    {
        //rend = GetComponent<Renderer>();
        //image.material.shader = Shader.Find("Unlit/OutLine");
    }

    // Update is called once per frame
    void Update()
    {
        // 水墨モードON
        if (LineController.is_inkMode)
        {
            
            //image.material.SetFloat("_Width", Width);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
            //Lnow = true;
            InkAmout.ImageOpen();
        }
        // 水墨モードOFF
        if (!LineController.is_inkMode)
        {
            
            //image.material.SetFloat("_Width", 0);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
            //Lnow = false;
            //if (Input.GetMouseButtonDown(0))    // クリックした瞬間
            //{
            //    rt_temp.y = 1.8f;   // サイズが小さくなる
            //}
            //if (Input.GetMouseButtonUp(0))      // 離した瞬間
            //{
            //    rt_temp.y = 2f;     // サイズが大きくなる
            //}

            InkAmout.ImageClose();

        }
        //rend.material.SetFloat("_Shininess", shininess);

    }
}

//mat = GetComponent<Renderer>().material;
//mat.SetColor("_EnergyColor", energyColor);
//mat.SetFloat("_Visibility", visibility);
//mat.SetFloat("_CollisionTime", -99f); /
