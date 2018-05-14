using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    public static GameSetting _instance;

    private void Awake()
    {
        //每次进入到冒险模式场景的时候都会调用一次改方法，都会初始化_instance
        _instance = this;
        ShowHWRModule();
    }

    //暂停面板
    public GameObject pausePanel;
    [HideInInspector] public bool isPause = false;

    //游戏结束面板
    public GameObject gameOverPanel;
    [HideInInspector] public bool isGameOver = false;

    //退出面板
    public GameObject exitGamePanel;
    private bool isShowExitPanel = false;


    public void StartGame()
    {
        SetGameStart();
        pausePanel.SetActive(false);
        isPause = false;
        SetHWRModule(true);

        //TODO 当退出和暂停面板使用同一个时,需要在这里设置isShowExitPanel为false
        isShowExitPanel = false;
    }

    public void PauseGame()
    {
        SetGamePause();
        pausePanel.SetActive(true);
        isPause = true;
        SetHWRModule(false);
    }

    public void SetGameOver(bool isOver)
    {
        SetGamePause();
        gameOverPanel.SetActive(isOver);
        isGameOver = isOver;
        SetHWRModule(false);
    }

    /// <summary>
    /// 通过传参来设置是否展示退出游戏面板
    /// </summary>
    /// <param name="isShow">ture表示显示退出面板并暂停游戏，false表示隐藏游戏面板并开始游戏</param>
    public void ShowExitGamePanel(bool isShow)
    {
        SetGamePause(isShow);
        exitGamePanel.SetActive(isShow);
    }

    public void SetExitGamePanel()
    {
        if (isShowExitPanel)
        {
            ShowExitGamePanel(false);
            isShowExitPanel = false;
            SetHWRModule(true); //显示手写模块
        }
        else
        {
            ShowExitGamePanel(true);
            isShowExitPanel = true;
            SetHWRModule(false); //隐藏手写模块
        }
    }

    //返回上一个场景
    public void BackToPreviousScene()
    {
        SetGameStart();
        BackHandler._instance.PopScene();
    }

    /// <summary>
    /// 方法重载，通过传参设置游戏是否暂停
    /// </summary>
    /// <param name="isPause">是否需要暂停，true为暂停，false为开始</param>
    private void SetGamePause(bool isPause)
    {
        if (isPause)
            SetGamePause();
        else
            SetGameStart();
    }

    /// <summary>
    /// 设置游戏暂停
    /// </summary>
    private void SetGamePause()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// 设置游戏开始
    /// </summary>
    private void SetGameStart()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// 手写模块设置
    /// </summary>
    /// <param name="isShow"></param>
    public void SetHWRModule(bool isShow)
    {
        if (isShow)
        {
            ShowHWRModule();
        }
        else
        {
            HideHWRModule();
        }
    }

    //隐藏手写模块
    public void HideHWRModule()
    {
        AndroidUtil.Call("removeHandWriteBroad");
    }

    //显示手写模块
    public void ShowHWRModule()
    {
        AndroidUtil.Call("addHandWriteBroad");
    }
}