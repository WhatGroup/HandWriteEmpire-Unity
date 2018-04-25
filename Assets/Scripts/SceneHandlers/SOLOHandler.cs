using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SOLOHandler : MonoBehaviour
{
    private static int count = 0;

    private List<short> pathData = new List<short>();

    public double FireTime;
    private double fireTime;
    private bool isDown = false;
    private bool isUp = false;

    public Text chinesePinYin;
    public Text chineseContent;
    public Text chineseCurrentPinYin;

    private String[] pinYinArray;
    private String[] contentArray;
    private int currentCharacter = 0;

    public ButtonStatusManagr btnManager;


    void Start()
    {
        //unity嵌入Android时显示手写板
        //        if (Application.platform == RuntimePlatform.Android)
        //        {
        //            ShowHWRModule();
        //        }
        fireTime = FireTime;
        ShowHWRModule();
        SetChineseInfo(new ChineseInfo("pin yin", "拼 音", "把两个或两个以上的音素结合起来成为一个复合的音"));
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
    }

    //测试后手写识别的功能
    public void TestHWRRec()
    {
        String result = HWRRecog();
//        effectTipText.text = "第" + ++count + "次识别:" + result;
        if (result != null && result.Length >= 1)
        {
//            contentText.text += result.Substring(0, 1) + " ";
            String[] results = result.Split(';');
            print("识别到的结果:" + results);
            foreach (var s in results)
            {
                print("进行对比" + s + "," + contentArray[currentCharacter]);
                if (s == contentArray[currentCharacter])
                {
                    btnManager.setAllActive();
                    AddCharacter(s);
                    currentCharacter++;
                    SetNewPinYin();
                    if (currentCharacter == contentArray.Length)
                    {
                        getNewChineseInfo();
                    }
                }
            }
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

    public void SetChineseInfo(ChineseInfo chinese)
    {
        currentCharacter = 0;
        pinYinArray = chinese.Pinyin.Split(' ');
        contentArray = chinese.Content.Split(' ');
        print("拼音: " + AssistFun.printArray(pinYinArray));
        print("内容: " + AssistFun.printArray(contentArray));

        chinesePinYin.text = chinese.Pinyin;
        chineseCurrentPinYin.text = pinYinArray[currentCharacter];
    }

    public void AddCharacter(String s)
    {
        chineseContent.text += s + " ";
    }

    public void SetNewPinYin()
    {
        chineseCurrentPinYin.text = pinYinArray[currentCharacter];
    }

    public void getNewChineseInfo()
    {
    }
}