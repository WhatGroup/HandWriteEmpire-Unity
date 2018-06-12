using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class MainUIManager : MonoBehaviour, HttpUtil.ICallBack
{
    public GameObject personCenterPanelGo;

    public AudioSource audioSource;

    public Image portrait;

    public Text attackValue;

    public Text defenseValue;

    public Text cureValue;

    public Toggle soundToggle;

    public Slider soundSlider;

    private float saveVolume;

    // Use this for initialization
    private void Awake()
    {
        if (GeneralUtils.IsStringEmpty(HttpUtil.Token))
            BackHandler._instance.GoToLogin();
        else
            AndroidUtil.Log(HttpUtil.Token);

        //soundSlider.interactable = false;
    }

    private void Start()
    {
        var sliderState = PrefsManager.SilderState;
        var volume = PrefsManager.Volume;
        soundSlider.value = volume;
        if (sliderState.Equals("true"))
        {
            soundSlider.interactable = true;
            soundToggle.isOn = false;
            audioSource.volume = volume;
        }
        else
        {
            soundSlider.interactable = false;
            soundToggle.isOn = true;
            audioSource.volume = 0;
            saveVolume = volume;
        }

        if (UserInfoManager._instance.GetUserInfo() != null)
            UpdateUserInfo();
        else
            RequestUserInfo();
    }


    private void RequestUserInfo()
    {
        HttpUtil.GetUserInfo(this, this);
    }

    public void OnRequestError(string error)
    {
        AndroidUtil.Toast("网络出错!\n" + error);
        StartCoroutine(LaterRequest(HttpUtil.RetryNetWorkTime));
    }

    private IEnumerator LaterRequest(float second)
    {
        yield return new WaitForSeconds(second);
        RequestUserInfo();
    }

    public void OnRequestSuccess(long responseCode, string response)
    {
//        print(response);
        //TODO 后台返回数据时多加一个字符,需要删除
        response = response.Remove(0, 1);
        AndroidUtil.Log(response);

        if (responseCode == 200)
        {
            var userInfo = JsonUtility.FromJson<UserInfo>(response);
            UserInfoManager._instance.SetUserInfo(userInfo);
            UpdateUserInfo();
        }
        else if (responseCode == 403)
        {
//            print(response);
            var errorInfo = JsonUtility.FromJson<ResponseInfo>(response);
            AndroidUtil.Log(errorInfo.message);
            AndroidUtil.Toast("登录过期，请重新登录");
            BackHandler._instance.GoToLogin();
        }
    }

    private void UpdateUserInfo()
    {
        var userInfo = UserInfoManager._instance.GetUserInfo();
        attackValue.text = userInfo.attackValue + "";
        defenseValue.text = userInfo.defenseValue + "";
        cureValue.text = userInfo.cureValue + "";
        //注意这里需要添加远程主机的地址，服务器返回的只有路径
        HttpUtil.ReplaceImageByNet(this, portrait, userInfo.portraitPath);
    }

    public void OnClickLogoutBtn()
    {
        HttpUtil.ClearToken();
        UserInfoManager._instance.ClearUserInfo();
        LevelDict.Instance.ClearLevelData();
        BackHandler._instance.GoToLogin();
    }

    public void OnClickClosePanelBtn()
    {
        personCenterPanelGo.SetActive(false);
    }

    public void OnClickOpenPanelBtn()
    {
        personCenterPanelGo.SetActive(true);
    }

    public void OnClickSaveBtn()
    {
        //TODO 保存用户数据
        personCenterPanelGo.SetActive(false);
    }

    public void OnClickNotepadBtn()
    {
        //TODO 进入错字本

        //将场景添加到返回栈，实现在Unity下通过返回键返回上一个场景
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("01_Notepad");
    }

    public void OnClickSoundToggle()
    {
        //UI做反了
        if (!soundToggle.isOn)
        {
            //打开声音
            soundSlider.interactable = true;
            audioSource.volume = saveVolume;
            PrefsManager.SilderState = "true";
        }
        else
        {
            //关闭声音
            soundSlider.interactable = false;
            audioSource.volume = 0;
            saveVolume = soundSlider.value;
            PrefsManager.SilderState = "false";
        }
    }

    public void OnDragSoundSlider()
    {
        audioSource.volume = soundSlider.value;
        PrefsManager.Volume = soundSlider.value;
    }
}