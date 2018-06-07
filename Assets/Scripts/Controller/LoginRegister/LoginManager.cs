using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour, HttpUtil.ICallBack
{
    public InputField accountInputField;
    public InputField pwdInputField;

    private void Awake()
    {
        var str = PlayerPrefs.GetString("Token");
        if (!GeneralUtils.IsStringEmpty(str))
        {
            HttpUtil.Token = str;
            BackHandler._instance.GoToMain();
        }
    }


    public void OnClickRegisterBtn()
    {
        BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
        BackHandler._instance.GoToRegister();
    }

    public void OnClickLoginBtn()
    {
        if (GeneralUtils.IsStringEmpty(accountInputField.text) || GeneralUtils.IsStringEmpty(pwdInputField.text))
        {
            AndroidUtil.Toast("用户或密码不能为空");
            return;
        }

        LoginAccount(accountInputField.text, pwdInputField.text);
    }

    private void LoginAccount(string account, string pwd)
    {
        HttpUtil.Login(this, account, pwd, this);
    }

    public void OnRequestError(string error)
    {
        AndroidUtil.Toast("网络出错!\n" + error);
    }

    public void OnRequestSuccess(long responseCode, string response)
    {
        print(response);
        response = response.Remove(0, 1);
        var responseInfo = JsonUtility.FromJson<ResponseInfo>(response);
        if (responseCode == 200)
        {
            HttpUtil.Token = responseInfo.attach;
            BackHandler._instance.GoToMain();
            AndroidUtil.Toast("登录成功");
        }
        else if (responseCode == 40)
        {
            AndroidUtil.Toast("用户名或密码错误");
        }
    }
}