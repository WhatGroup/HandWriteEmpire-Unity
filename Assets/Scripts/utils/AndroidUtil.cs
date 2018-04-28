using System;
using UnityEngine;
using Application = UnityEngine.Application;

//Android专用工具
public class AndroidUtil
{
    public static void Call(string methedName, params object[] args)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            var mainActivity =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            mainActivity.Call(methedName, args);
        }
    }

    public static T Call<T>(string methedName, params object[] args)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            var mainActivity =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            return (T) Convert.ChangeType(mainActivity.Call<T>(methedName, args), typeof(T));
        }
        else
        {
            return (T) Convert.ChangeType(null, typeof(T));
        }
    }

    public static void CallStatic(string methedName, params object[] args)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            var mainActivity =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            mainActivity.CallStatic(methedName, args);
        }
    }

    public static T CallStatic<T>(string methedName, params object[] args)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            var mainActivity =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            return (T) Convert.ChangeType(mainActivity.CallStatic<T>(methedName, args), typeof(T));
        }

        else
        {
            return (T) Convert.ChangeType(null, typeof(T));
        }
    }

    public static void Toast(string content)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            var mainActivity =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");

            var toast = new AndroidJavaClass("android.widget.Toast");
            mainActivity.Call("runOnUiThread",
                new AndroidJavaRunnable(() =>
                {
                    toast.CallStatic<AndroidJavaObject>("makeText", mainActivity, content,
                        toast.GetStatic<int>("LENGTH_SHORT")).Call("show");
                }));
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log(content);
        }
    }

    //相对于屏幕中心的位置
    public static void Toast(string content, int x, int y)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            var mainActivity =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");

            var toast = new AndroidJavaClass("android.widget.Toast");
            var gravity = new AndroidJavaClass("android.view.Gravity");
            mainActivity.Call("runOnUiThread",
                new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject t = toast.CallStatic<AndroidJavaObject>("makeText", mainActivity, content,
                        toast.GetStatic<int>("LENGTH_SHORT"));
                    t.Call("setGravity", gravity.GetStatic<int>("CENTER"), x, y);
                    t.Call("show");
                }));
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log(content);
        }
    }


    public static void Log(string content)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            var log = new AndroidJavaClass("android.util.Log");
            log.CallStatic<int>("d", "HandWriteEmpire", content);
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log(content);
        }
    }
}