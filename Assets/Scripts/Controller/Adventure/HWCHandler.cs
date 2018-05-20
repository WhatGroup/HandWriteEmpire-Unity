using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HWCHandler : MonoBehaviour
{
    //书写冷却时间
    public double WriteFireTime;
    private double writeFireTime;


    private bool isStartWrite = false;
    private bool isUp = false;

    void Start()
    {
        writeFireTime = WriteFireTime;
    }

    void Update()
    {
        if (isStartWrite && isUp)
        {
            writeFireTime -= Time.deltaTime;
            if (writeFireTime <= 0)
            {
                writeFireTime = WriteFireTime;
                isStartWrite = false;
                isUp = false;
                HWRRec();
            }
        }
        else
        {
            writeFireTime = WriteFireTime;
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
            writeFireTime = WriteFireTime;
        }
        else if ("Up".Equals(state))
        {
            isUp = true;
        }
    }

    //手写识别的功能
    public void HWRRec()
    {
        //TODO 限制输入的个数以及实现修改功能
        CallHWRRec();
    }

    //手写模块识别结果回调，在Anroid那边调用
    public void OnGetRecResult(string results)
    {
        AndroidUtil.Log("识别到的结果:" + results);
        if (results != null && results.Length >= 1)
        {
            var resultArr = results.Split(';');
            if (resultArr.Length > 0) WordHandler._instance.SetCharacter(resultArr[0]);
        }
    }

    //获取Android手写识别后的结果
    public void CallHWRRec()
    {
        AndroidUtil.Call("hwrRec");
    }

    //用于Unity下调试手写识别结果反馈
    public void TestHWRRec(string results)
    {
        AndroidUtil.Log("识别到的结果:" + results);
        if (results != null && results.Length >= 1) WordHandler._instance.SetCharacter(results.Substring(0, 1));
    }
}