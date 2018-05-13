using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//正在游戏的时候点击返回按钮
public class GamingBackListener : MonoBehaviour
{
    private bool isShowExitPanel = false;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isShowExitPanel == false)
            {
                isShowExitPanel = true;
                GameSetting._instance.ShowExitGamePanel(true);
            }

            else
            {
                isShowExitPanel = false;
                GameSetting._instance.ShowExitGamePanel(false);
            }
        }
    }
}
/*public class NormalBackListener : MonoBehaviour
{
    void Update()
    {
        //监听返回按钮，所有的场景都需要添加该脚本
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            BackHandler._instance.PopScene();
        }
    }
}*/