using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//正在游戏的时候点击返回按钮
public class GamingBackListener : MonoBehaviour
{
    void Update()
    {
        //不同状态处理返回按钮
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameSetting._instance.isGameOver)
            {
                //游戏结束，退出游戏
                GameSetting._instance.BackToPreviousScene();
            }
            else
            {
                //游戏正在进行中，点返回按钮，出现退出对话框
                GameSetting._instance.SetPauseGamePanel();
            }
        }
    }
}