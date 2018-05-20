using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackHandler
{
    public static BackHandler _instance = new BackHandler();

    //场景栈，存放加载过的场景
    private Stack<string> sceneOrder = new Stack<string>();
    
    public void PopScene()
    {
        if (sceneOrder.Count > 0)
        {
            SceneManager.LoadScene(sceneOrder.Pop());
        }
        else
        {
            //当sceneOrder中没有场景时推出应用
            Application.Quit();
        }
    }

    public void AddScene(String sceneName)
    {
        sceneOrder.Push(sceneName);
    }
}