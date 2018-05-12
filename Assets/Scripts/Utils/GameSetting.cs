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
    //暂停后显示的背景
    public GameObject pausePanel;
    public GameObject gameOverPanel;

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

    public void GameOver(Boolean result)
    {
        SetGamePause();
        gameOverPanel.SetActive(result);
    }

    //返回上一个场景
    public void BackToPreviousScene()
    {
        SetGameStart();
        BackHandler._instance.PopScene();
    }
    private void SetGamePause()
    {
        Time.timeScale = 0;
    }
    private void SetGameStart()
    {
        Time.timeScale = 1;
    }
}