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
    public Image portrait;

    public Text attackValue;

    public Text defenseValue;

    public Text cureValue;

    // Use this for initialization
    void Start()
    {
        if (UserInfoManager._instance.GetUserInfo() != null)
        {
            UpdateUserInfo();
        }
        else
        {
            RequestUserInfo();
        }
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
        //TODO 不知道什么原因后台返回数据时多加一个字符
        response = response.Remove(0, 1);
        if (responseCode == 200)
        {
//            response = response.Replace("\\/", "/");
//            response = response.Replace("\\u", "");
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
        StartCoroutine(UpdatePortrait(HttpUtil.RemotePath + userInfo.portraitPath));
    }

    IEnumerator UpdatePortrait(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.Send();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                int width = 1920;
                int height = 1080;
                byte[] results = www.downloadHandler.data;
                Texture2D texture = new Texture2D(width, height);
                texture.LoadImage(results);
                yield return new WaitForSeconds(0.01f);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));
                portrait.sprite = sprite;
                yield return new WaitForSeconds(0.01f);
                Resources.UnloadUnusedAssets();
            }
        }
    }
}