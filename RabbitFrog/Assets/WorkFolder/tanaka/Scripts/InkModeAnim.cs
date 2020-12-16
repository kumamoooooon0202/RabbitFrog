using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkModeAnim : MonoBehaviour
{

    [SerializeField] GameObject InkMode;
    [SerializeField] private bool modeSwitch = false;

    Animator anim;

    private string isModeSwitch = "IsModeSwitch";

    public InkMode _inkMode;

    // Start is called before the first frame update
    void Start()
    {
        anim = InkMode.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InkAnim()
    {
        modeSwitch = !modeSwitch;
        //Debug.Log("isModeSwitchが" + modeSwitch + "isModeSwitchが" + modeSwitch);
        if (modeSwitch)
        {
            anim.SetBool(isModeSwitch, true);
        }
        if (!modeSwitch)
        {
            anim.SetBool(isModeSwitch, false);
        }
    }
}
