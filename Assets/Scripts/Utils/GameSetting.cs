using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    public static GameSetting _instance;

    void Awake()
    {
        _instance = this;
    }

    //暂停面板
    public GameObject pausePanel;

    //游戏结束面板
    public GameObject gameOverPanel;

    //退出面板
    public GameObject exitGamePanel;


    public void StartGame()
    {
        SetGameStart();
        pausePanel.SetActive(false);
    }

    public void PauseGame()
    {
        SetGamePause();
        pausePanel.SetActive(true);
    }

    public void GameOver(bool result)
    {
        SetGamePause();
        gameOverPanel.SetActive(result);
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
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
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
}