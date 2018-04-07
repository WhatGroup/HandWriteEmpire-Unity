using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SOLOHandler : MonoBehaviour
{
    public Text effectTipText;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
//            Input.backButtonLeavesApp = true;
            ShowHWRModule();
        }
    }

    void Update()
    {
        if (Application.platform== RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
        {
            HideHWRModule();
        }
    }
    public void HideHWRModule()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("hideHWRModule");
    }

    private void ShowHWRModule()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("showHWRModule");
    }

    public void ShowEffect(String result)
    {
        effectTipText.text = result;
    }
}