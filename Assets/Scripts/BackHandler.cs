using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackHandler : MonoBehaviour
{
    public static BackHandler _instance;

    //已加载的场景
    private Stack<string> sceneOrder = new Stack<string>();

    void Awake()
    {
        _instance = this;
    }

    public void PopScene()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
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
    }

    public void AddScene(String sceneName)
    {
        sceneOrder.Push(sceneName);
    }
}