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

    public ButtonStatusManagr btnManager;
    private ChineseInfo chinese = new ChineseInfo("pinyin", "拼音", "把两个或两个以上的音素结合起来成为一个复合的音");


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


        if (isDown && isUp)
        {
            fireTime -= Time.deltaTime;
            if (fireTime <= 0)
            {
                TestHWRRec();
                fireTime = FireTime;
                isDown = false;
                isUp = false;
                btnManager.setAllActive();
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
        AndroidUtil.Call("removeHandWriteBroad");
    }

    private void ShowHWRModule()
    {
        AndroidUtil.Call("addHandWriteBroad");
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
        return AndroidUtil.Call<String>("hwrRec");
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