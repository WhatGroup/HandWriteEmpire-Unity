using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainHandler : MonoBehaviour {

    public void OnClickDictionaryBtn()
    {
        //将场景添加到返回栈，实现在Unity下通过返回键返回上一个场景
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("02_Dictionary");
    }
    public void OnClickAdventureBtn()
    {
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("06_LevelSelect");
    }
    public void OnClickTrainBtn()
    {
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("04_Train");
    }
    public void OnClickRoleManagerBtn()
    {
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("05_RoleManager");
    }
}
