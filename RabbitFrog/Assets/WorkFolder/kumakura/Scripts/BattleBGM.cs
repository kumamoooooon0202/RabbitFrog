using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBGM : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip[] sound;


    // Start is called before the first frame update
    void Start()
    {
        audio.PlayOneShot(sound[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
