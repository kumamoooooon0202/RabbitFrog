using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



//namespace UnityEngine { }

public class optionCharCon : MonoBehaviour
{

    [SerializeField]
    public Image randomImage;
    [SerializeField] private Sprite[] images = new Sprite[3];
    public bool hf = true;
    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        OnSceneLoaded(SceneManager.GetActiveScene().name);
    }

   

    public void OnSceneLoaded(string scene)
    {
        if (scene == "OptionScene 1")
        {
            int num = Random.Range(0, images.Length);
            randomImage.sprite = images[num];
        } 
        //else if(hf == true)
        //{
            
        //}
        //else
        //{

        //}
     
    }



    //public void ChangeChara()
    //{
    //    if(SceneManager.GetActiveScene().name == "optionscene")
    //    {
    //        int num = Random.Range(0, 3);
    //        randomImage.sprite = images[num];

    //    }

    //int num = Random.Range(0, 2);
    //randomImage.sprite = images[num];
}
    
    

