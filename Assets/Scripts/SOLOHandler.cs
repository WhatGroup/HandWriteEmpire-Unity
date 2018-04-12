using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SOLOHandler : MonoBehaviour
{
    public Text effectTipText;
    public Text contentText;

    private static int count = 0;

    private List<short> pathData = new List<short>();

    public double FireTime;
    private double fireTime;
    private bool isDown = false;
    private bool isUp = false;


    void Start()
    {
        //unity嵌入Android时显示手写板
        //        if (Application.platform == RuntimePlatform.Android)
        //        {
        //            ShowHWRModule();
        //        }
        fireTime = FireTime;
        ShowHWRModule();
    }

    void Update()
    {
        //unity嵌入Android隐藏手写板
        if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
        {
            HideHWRModule();
        }


        //直接在unity中获取点的位置，出现的问题得多出一些点，在点击按钮的时候的那个笔记也被记录下来
        /*      if (Input.touchCount > 0)
              {
                  Touch touch = Input.GetTouch(0);
                  Vector2 v = touch.position;
                  switch (touch.phase)
                  {
                      case TouchPhase.Began:
      //                    pathData.Add((short) v.x);
      //                    pathData.Add((short) v.y);
                          break;
                      case TouchPhase.Moved:

                          pathData.Add((short) v.x);
                          pathData.Add((short) v.y);
                          break;
                      case TouchPhase.Ended:
                          pathData.Add(-1);
                          pathData.Add(0);
                          break;
                  }

                  Debug.Log(v.x + " " + v.y);
              }*/

        //根据时间间隔提交

        /* if (Input.touchCount > 0)
         {
             Touch touch = Input.GetTouch(0);
             switch (touch.phase)
             {
                 case TouchPhase.Began:
                     isWrited = true;
                     break;
             }
         }
 
         if (isWrited)
         {
             if (Input.touchCount > 0)
             {
                 fireTime = FireTime;
             }
             else
             {
                 fireTime--;
                 if (fireTime <= 0)
                 {
                     TestHWRRec();
                     fireTime = FireTime;
                     isWrited = false;
                 }
             }
         }*/
        if (isDown && isUp)
        {
            fireTime -= Time.deltaTime;
            if (fireTime <= 0)
            {
                TestHWRRec();
                fireTime = FireTime;
                isDown = false;
                isUp = false;
            }
        }
        else
        {
            fireTime = FireTime;
        }
    }

    public void SetTimerStart(String state)
    {
        if ("Down".Equals(state))
        {
            isDown = true;
        }
        else if ("Move".Equals(state))
        {
            isUp = false;
            fireTime = FireTime;
        }
        else if ("Up".Equals(state))
        {
            isUp = true;
        }
    }

    public void HideHWRModule()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("removeHandWriteBroad");
    }

    private void ShowHWRModule()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("addHandWriteBroad");
    }

    public void ShowEffect(String result)
    {
        effectTipText.text = result;
    }

    public void TestHWRRec()
    {
        String result = HWRRecog();
        effectTipText.text = "第" + ++count + "次识别:" + result;
        if (result != null && result.Length >= 1)
        {
            contentText.text += result.Substring(0, 1) + " ";
        }

        pathData.Clear();
    }

    public String HWRRecog()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        return jo.Call<String>("hwrRec");
    }

    private void PrintArray(short[] ss)
    {
        string result = "";
        foreach (var s in ss)
        {
            result += s + ",";
        }

        print(result.Substring(0, result.Length - 1));
    }
}