using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackHandler
{
    public static BackHandler _instance = new BackHandler();

    private BackHandler()
    {
    }

    //场景栈，存放加载过的场景
    private Stack<string> sceneOrder = new Stack<string>();

    public void PopScene()
    {
        if (sceneOrder.Count > 0)
            SceneManager.LoadScene(sceneOrder.Pop());
        else
            Application.Quit();
    }

    public void ClearAllScene()
    {
        sceneOrder.Clear();
    }

    public void AddScene(string sceneName)
    {
        sceneOrder.Push(sceneName);
    }

    public void GoToLogin()
    {
        //返回登录界面
        ClearAllScene();
        SceneManager.LoadScene("00_Login");
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("01_Main");
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene("00_Register");
    }
}