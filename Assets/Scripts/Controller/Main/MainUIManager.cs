using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class MainUIManager : MonoBehaviour, HttpUtil.ICallBack
{
    public GameObject personCenterPanelGo;

    public Image portrait;

    public Text attackValue;

    public Text defenseValue;

    public Text cureValue;

    // Use this for initialization
    private void Awake()
    {
        if (GeneralUtils.IsStringEmpty(HttpUtil.Token))
        {
            BackHandler._instance.GoToLogin();
        }
        else
        {
            AndroidUtil.Log(HttpUtil.Token);
        }
    }

    private void Start()
    {
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
    }
}