using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterManager : MonoBehaviour, HttpUtil.ICallBack
{
    public InputField AccountInputField;
    public InputField pwdInputField;

    public void OnClickRegisterBtn()
    {
        if (GeneralUtils.IsStringEmpty(AccountInputField.text) || GeneralUtils.IsStringEmpty(pwdInputField.text))
        {
            AndroidUtil.Toast("用户或密码不能为空");
            return;
        }

        RegisterAccount(AccountInputField.text, pwdInputField.text);
    }

    private void RegisterAccount(string account, string pwd)
    {
        HttpUtil.Register(this, account, pwd, this);
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
            AndroidUtil.Toast("注册成功");
        }
        else if (responseCode == 409)
        {
            AndroidUtil.Toast("用户名重复");
        }
    }
}