using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SOLOHandler : MonoBehaviour
{
    public double FireTime;
    private double fireTime;
    private bool isStartWrite = false;
    private bool isUp = false;

    public ChineseInfo[] infos;
    public int currentChinese = 0;
    public Text chinesePinYin;
    public Text chineseContent;
    public Text chineseCurrentPinYin;

    private string[] pinYinArray;
    private string[] contentArray;
    private int currentCharacter = 0;
    private string regResult;

    public ButtonStatusManagr btnManager;


    private void Start()
    {
        //unity嵌入Android时显示手写板
        //        if (Application.platform == RuntimePlatform.Android)
        //        {
        //            ShowHWRModule();
        //        }
        RequestInfo();

        fireTime = FireTime;
        ShowHWRModule();
        UpdateChineseInfo(infos[currentChinese]);
    }

    private void RequestInfo()
    {
        infos = new ChineseInfo[4];
        infos[0] = new ChineseInfo("ni hao", "你 好", "用于有礼貌的打招呼或表示与人见面时的问候");
        infos[1] = new ChineseInfo("ke ji", "科 技", "社会上习惯于把科学和技术连在一起，统称为“科技”。实际二者既有密切联系，又有重要区别。科学解决理论问题，技术解决实际问题");
        infos[2] = new ChineseInfo("xian zai", "现 在", "现世,今生;眼前一刹那");
        infos[3] = new ChineseInfo("wei lai", "未 来", "从现在往后的时间");
    }

    private void Update()
    {
        //unity嵌入Android隐藏手写板
        if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape)) HideHWRModule();


        if (isStartWrite && isUp)
        {
            fireTime -= Time.deltaTime;
            if (fireTime <= 0)
            {
                fireTime = FireTime;
                isStartWrite = false;
                isUp = false;
                TestHWRRec();
            }
        }
        else
        {
            fireTime = FireTime;
        }
    }

    public void SetTimerStart(string state)
    {
        if ("Down".Equals(state))
        {
            //当按下的时候表示开始写字
            isStartWrite = true;
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


    //测试手写识别的功能
    public void TestHWRRec()
    {
        var results = HWRRecog();
//        effectTipText.text = "第" + ++count + "次识别:" + result;
        if (results != null && results.Length >= 1)
        {
//            contentText.text += result.Substring(0, 1) + " ";
            var resultArr = results.Split(';');
            AndroidUtil.Log("识别到的结果:" + results);
            if (resultArr.Length > 0)
            {
                AddCharacter(resultArr[0]);
                currentCharacter++;
                if (currentCharacter == contentArray.Length)
                    btnManager.SetAllInteractable();
                else
                    SetNewPinYin();
            }
        }
    }

    public string HWRRecog()
    {
        return AndroidUtil.Call<string>("hwrRec");
    }

    public void ResultJudge()
    {
        if (!chineseContent.text.Equals(infos[currentChinese].Content))
            AndroidUtil.Toast("输入错误!!!\n" + "实际: " + chineseContent.text + "\n输入: " +
                              infos[currentChinese].Content);
        else
        {
            AndroidUtil.Toast("攻击效果!!!");
        }

        currentChinese++;
        if (currentChinese == infos.Length)
        {
            //TODO 游戏结束
            AndroidUtil.Toast("游戏结束");
            HideHWRModule();
        }
        else
        {
            UpdateChineseInfo(infos[currentChinese]);
        }
    }

    public void UpdateChineseInfo(ChineseInfo chinese)
    {
        currentCharacter = 0;
        pinYinArray = chinese.Pinyin.Split(' ');
        contentArray = chinese.Content.Split(' ');
        AndroidUtil.Log("拼音: " + GeneralTools.printArray(pinYinArray));
        AndroidUtil.Log("内容: " + GeneralTools.printArray(contentArray));

        chinesePinYin.text = chinese.Pinyin;
        chineseContent.text = "";
        chineseCurrentPinYin.text = pinYinArray[currentCharacter];
    }

    public void AddCharacter(string s)
    {
        if (chineseContent.text.Length > 0)
            chineseContent.text += " " + s;
        else
            chineseContent.text += s;
    }

    public void SetNewPinYin()
    {
        chineseCurrentPinYin.text = pinYinArray[currentCharacter];
    }

    public void ShowDetialContent()
    {
        AndroidUtil.Toast(infos[currentChinese].Detail, 0, 0);
    }
}