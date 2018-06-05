using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.WindowsStandalone;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

public class HttpUtil
{
    public const string RemotePath = "http://139.199.88.206/";
    public const string LoginPath = RemotePath + "api/auth/login.php";
    public const string RegisterPath = RemotePath + "api/auth/register.php";
    public const string GetUserInfoPath = RemotePath + "api/get/user_info.php";
    public const string GetFindWordPath = RemotePath + "api/get/find_word.php";
    public const string PostUserInfoPath = RemotePath + "api/post/user_info.php";
    public const string PostUserLevelInfosPath = RemotePath + "api/post/user_level_infos.php";
    public const string PostErrorWordInfosPath = RemotePath + "api/post/error_word_infos.php";


    private static string token = "";
    private static int retryNetWorkTime = 5;

    public static string Token
    {
        get { return token; }
        set
        {
            token = value;
            //保存到SharedPreferences，下次登录的时候不用获取
            PlayerPrefs.SetString("Token", token);
        }
    }

    //清除token
    public static void ClearToken()
    {
        Token = "";
        PlayerPrefs.DeleteKey("Token");
    }

    public static int RetryNetWorkTime
    {
        get { return retryNetWorkTime; }
        set { retryNetWorkTime = value; }
    }


    public interface ICallBack
    {
        void OnRequestError(string error);
        void OnRequestSuccess(long responseCode, string response);
    }

    //注册
    public static void Register(MonoBehaviour behaviour, string acccount, string paw, ICallBack callBack)
    {
        var registerUrl = RegisterPath + "?account=" + acccount + "&paw=" + paw;
        behaviour.StartCoroutine(GetInfo(registerUrl, callBack));
    }

    //登录
    public static void Login(MonoBehaviour behaviour, string acccount, string paw, ICallBack callBack)
    {
        var loginUrl = LoginPath + "?account=" + acccount + "&paw=" + paw;
        behaviour.StartCoroutine(GetInfo(loginUrl, callBack));
    }

    //获取用户数据
    public static void GetUserInfo(MonoBehaviour behaviour, ICallBack callBack)
    {
        if (GeneralUtils.IsStringEmpty(Token))
        {
            BackHandler._instance.GoToLogin();
        }
        else
        {
            var userInfoUrl = GetUserInfoPath + "?token=" + Token;
            behaviour.StartCoroutine(GetInfo(userInfoUrl, callBack));
        }
    }

    //获得关卡数据
    public static void GetUserLevelInfos(MonoBehaviour behaviour, ICallBack callBack)
    {
        var userLevelInfoRelativePath = UserInfoManager._instance.GetUserInfo().userLevelInfosPath;
        var wordInfoPath = RemotePath + userLevelInfoRelativePath;
        behaviour.StartCoroutine(GetInfo(wordInfoPath, callBack));
    }

    //获取单词数据
    public static void GetWordInfo(MonoBehaviour behaviour, ICallBack callBack)
    {
        var wordInfoRelativePath = LevelDict.Instance.GetLevelInfo(LevelDict.Instance.SelectLevel).wordInfoPath;
        var wordInfoPath = RemotePath + wordInfoRelativePath;
        behaviour.StartCoroutine(GetInfo(wordInfoPath, callBack));
    }

    public static void GetUserErrorWordInfos(MonoBehaviour behaviour, ICallBack callBack)
    {
    }


    public static void PostUserInfo(MonoBehaviour behaviour)
    {
        var json = JsonUtility.ToJson(UserInfoManager._instance.GetUserInfo());
        behaviour.StartCoroutine(PostInfo(PostUserInfoPath, json));
    }

    public static void PostUserLevelInfos(MonoBehaviour behaviour)
    {
        var json = JsonUtility.ToJson(new UserLevelInfoList(LevelDict.Instance.GetUserLevelInfos()));

        var iparams = new List<IMultipartFormSection>();
        iparams.Add(new MultipartFormDataSection("token", Token));
        iparams.Add(new MultipartFormDataSection("userLevelInfos", json));
        behaviour.StartCoroutine(PostInfo(PostUserLevelInfosPath, iparams));
    }

    public static void PostErrorWordInfos(MonoBehaviour behaviour)
    {
        var json = JsonUtility.ToJson(new UserErrorWordInfos(ScoreManager._instance.errorWordList));
        var iparams = new List<IMultipartFormSection>();
        iparams.Add(new MultipartFormDataSection("token", Token));
        iparams.Add(new MultipartFormDataSection("userErrorWordInfos", json));
        behaviour.StartCoroutine(PostInfo(PostErrorWordInfosPath, iparams));
    }

    private static IEnumerator PostInfo(string url, string json)
    {
        using (var www = UnityWebRequest.Post(PostUserInfoPath, json))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError) AndroidUtil.Toast("网络用户数据出错" + www.error);
        }
    }

    private static IEnumerator PostInfo(string url, List<IMultipartFormSection> iparams)
    {
        using (var www = UnityWebRequest.Post(PostUserInfoPath, iparams))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError) AndroidUtil.Toast("网络关卡数据出错" + www.error);
        }
    }

    private static IEnumerator GetInfo(string url, ICallBack callBack)
    {
        using (var www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError)
                callBack.OnRequestError(www.error);
            else
                callBack.OnRequestSuccess(www.responseCode, www.downloadHandler.text);
        }
    }
}