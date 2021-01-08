using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ConfirmCanvas : MonoBehaviour
{
    [SerializeField]
    Effect_Sketch effect_sketch;

    Canvas confirmCanvas;

    //------------------------------------------------------------
    [SerializeField] AudioClip yesSE;
    [SerializeField] AudioClip noSE;
    AudioSource audioSource;
    //------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        confirmCanvas = GetComponent<Canvas>();

        //21/01/08
        //------------------------------------------------------------
        audioSource = GetComponent<AudioSource>();
        //------------------------------------------------------------
    }

    /// <summary>
    /// Scene移動
    /// </summary>
    public void NextScene()
    {
        StartCoroutine(effect_sketch.NextScene(StageSelectControl.NextScene));
        DeckManager.SetDeckObject();
        confirmCanvas.rootCanvas.enabled = !confirmCanvas.rootCanvas.enabled;

        //------------------------------------------------------------
        audioSource.PlayOneShot(yesSE);
        //------------------------------------------------------------
    }

    public void Cencle()
    {
        confirmCanvas.rootCanvas.enabled = !confirmCanvas.rootCanvas.enabled;
        
        //------------------------------------------------------------
        audioSource.PlayOneShot(noSE);
        //------------------------------------------------------------
    }
}
