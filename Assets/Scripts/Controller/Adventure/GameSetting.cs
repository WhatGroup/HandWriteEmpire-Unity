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
    }

    public GameObject VictoryPanel;
    public GameObject FailPanel;

    //TODO 由于暂停面板和退出面板是相同的，后续可以直接合并成一个
    //暂停面板
    public GameObject pausePanel;
    [HideInInspector] public bool isPause = false;


    //游戏结束面板
    public GameObject gameOverPanel;
    [HideInInspector] public bool isGameOver = false;


    //当前手写板的状态
    private bool isShowHWR = false;

    //当前是否在播放攻击或者失败动画
    private bool playAnimState = false;

    public bool PlayAnimState
    {
        get { return playAnimState; }
        set { playAnimState = value; }
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
    private void ShowPauseGamePanel(bool isShow)
    {
        isPause = isShow;
        SetGamePause(isShow);
        pausePanel.SetActive(isShow);
    }

    public void SetPauseGamePanel()
    {
        if (isPause)
        {
            ShowPauseGamePanel(false);
            if (PlayAnimState == false) SetHWRModule(true); //如果当前没有在播放攻击或者失败动画则显示手写模块
        }
        else
        {
            ShowPauseGamePanel(true);
            SetHWRModule(false); //隐藏手写模块
        }
    }

    //返回上一个场景
    public void BackToPreviousScene()
    {
        SetGameStart();
        BackHandler._instance.PopScene();
    }

    public void RestartCurrentScene()
    {
        SetGameStart();
        //重新加载此关
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        //添加判断，如果之前的状态和设置的状态相同，则不修改
        if (isShow != isShowHWR)
        {
            isShowHWR = isShow;
            if (isShow)
                ShowHWRModule();
            else
                HideHWRModule();
        }
    }

    //隐藏手写模块
    private void HideHWRModule()
    {
        AndroidUtil.Call("removeHandWriteBroad");
    }

    //显示手写模块
    private void ShowHWRModule()
    {
        AndroidUtil.Call("addHandWriteBroad");
    }
}