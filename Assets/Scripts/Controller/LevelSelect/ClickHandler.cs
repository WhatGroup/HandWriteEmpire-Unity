using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickHandler : MonoBehaviour
{
    [HideInInspector]
    public string levelWordInfoUrl="";

    public void OnClickCurrenLevel()
    {
        URLTransfer._instance.url = levelWordInfoUrl;
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("03_Adventure");
    }

    public void OnClickOkLevel()
    {
        URLTransfer._instance.url = levelWordInfoUrl;
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("03_Adventure");
    }

    public void OnClickLockLevel()
    {
        AndroidUtil.Toast("关卡未解锁");
    }
}