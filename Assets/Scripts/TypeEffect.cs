using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public GameObject endCursor;
    public int charPerSeconds;
    public string targetMsg;

    Text msgTxt;
    AudioSource audioSource;
    int index;
    float interval;
    public bool isAnim;

    void Awake()
    {
        msgTxt = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg)
    {
        if(isAnim) {
            msgTxt.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgTxt.text = "";
        index = 0;
        endCursor.SetActive(false);
        //start anim
        interval = 1.0f / charPerSeconds;

        isAnim = true;

        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        //end anim
        if(msgTxt.text == targetMsg) {
            EffectEnd();
            return;
        }

        msgTxt.text += targetMsg[index];
        //sound
        if(targetMsg[index] != ' ' || targetMsg[index] !=  '.')
            audioSource.Play();

        index++;
        //recursive
        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        isAnim = false;
        endCursor.SetActive(true);
    }
}
