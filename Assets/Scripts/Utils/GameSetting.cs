using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    //暂停后显示的背景
    public GameObject pausePanel;
    public void StartGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
}