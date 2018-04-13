using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidUtil
{
    private static AndroidJavaObject mainActivity =
        new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");


    public static void Call(string methedName, params object[] args)
    {
        if (Application.platform == RuntimePlatform.Android)
            mainActivity.Call(methedName, args);
    }

    public static T Call<T>(string methedName, params object[] args)
    {
        if (Application.platform == RuntimePlatform.Android)
            return (T) Convert.ChangeType(mainActivity.Call<T>(methedName, args), typeof(T));
        else
            return (T) Convert.ChangeType(null, typeof(T));
    }

    public static void CallStatic(string methedName, params object[] args)
    {
        if (Application.platform == RuntimePlatform.Android)
            mainActivity.CallStatic(methedName, args);
    }

    public static T CallStatic<T>(string methedName, params object[] args)
    {
        if (Application.platform == RuntimePlatform.Android)
            return (T) Convert.ChangeType(mainActivity.CallStatic<T>(methedName, args), typeof(T));
        else
            return (T) Convert.ChangeType(null, typeof(T));
    }
}