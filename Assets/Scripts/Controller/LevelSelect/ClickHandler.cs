using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour
{
    public Button currentLevelButton;
    public Button okLevelButton;
    public Button lockLevelButton;
    [HideInInspector] public string levelWordInfoUrl = "";

    public void AddOnClickCurrenLevelListener(int level)
    {
        currentLevelButton.onClick.AddListener(delegate() { OnClickNormal(level); });
    }

    public void AddOnClickOkLevelListener(int level)
    {
        okLevelButton.onClick.AddListener(delegate() { OnClickNormal(level); });
    }


    public void AddOnClickLockLevelListener(int level)
    {
        lockLevelButton.onClick.AddListener(delegate() { OnClickLock(level); });
    }

    private void OnClickNormal(int level)
    {
        LevelDict.Instance.SelectLevel = level;
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        
        SceneManager.LoadScene("03_Loading");
    }

    private void OnClickLock(int level)
    {
        AndroidUtil.Toast("关卡" + level + " 未解锁");
    }
}