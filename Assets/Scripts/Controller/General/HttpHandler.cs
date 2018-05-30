using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

public class HttpHandler : MonoBehaviour
{
    private const string DoMain = "http://127.0.0.1/";

    //是否使用网络
    public bool isNetwork = true;

    private const string GetInfosURL = DoMain + "data/";
    private const string GetJsonFilesURL = "data/";
    [HideInInspector] public static HttpHandler _instance;


    //TODO 测试用，随机生成的范围
    public int minFileValue;
    public int maxFileValue;

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }


    public interface ICallBack
    {
        void OnRequestError(string error);
        void OnRequestSuccess(string response);
    }

    //获得关卡数据
    public void GetLevelInfos(ICallBack callBack)
    {
        //随机请求一个文件
        if (isNetwork)
            GetByNetWork(DoMain + UserInfoManager._instance.GetLevelInfosUri(), callBack);
        else
            GetByLocal(GetJsonFilesURL + "levelInfos", callBack);

        AndroidUtil.Log((isNetwork ? "网络" : "本地") + "加载\n文件名: " + "levelInfo.json");
    }

    //TODO 请求数据处理,后续考虑是网络加载还是本地加载
    public void GetWordInfo(ICallBack callBack)
    {
        var jsonFileName = "";

        if (LevelDict.Instance.IsEmpty() && LevelDict.Instance.SelectLevel == 0)
        {
            //随机请求一个文件
            var fileName = new Random().Next(maxFileValue - minFileValue + 1) + minFileValue;
            jsonFileName = fileName + "";
        }
        else
        {
            //当LevelDict的内容不为空，并且其保存的SelectLevel不等于0时，SelectLevel之前选中的关卡数
            jsonFileName = LevelDict.Instance.SelectLevel + "";
        }

        if (isNetwork)
            GetByNetWork(GetInfosURL + jsonFileName + ".json", callBack);
        else
            GetByLocal(GetJsonFilesURL + jsonFileName, callBack);

        AndroidUtil.Log((isNetwork ? "网络" : "本地") + "加载\n文件名: " + jsonFileName + ".json");
    }

    private void GetByNetWork(string url, ICallBack callBack)
    {
        StartCoroutine(GetText(url, callBack));
    }

    private IEnumerator GetText(string url, ICallBack callBack)
    {
        using (var www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
                callBack.OnRequestError(www.error);
            else
                callBack.OnRequestSuccess(www.downloadHandler.text);
        }
    }

    private void GetByLocal(string file, ICallBack callBack)
    {
        var ta = Resources.Load<TextAsset>(file);
        if (ta == null || ta.text.Equals(""))
            callBack.OnRequestError("数据文件不存在或者内容为空");
        else
            callBack.OnRequestSuccess(ta.text);
    }

    public void SaveLevelInfo()
    {
        var infos = new LevelInfos();
        infos.levelList = new List<LevelInfo>();
        var count = LevelDict.Instance.GetCount();
        for (var i = 1; i <= count; i++)
        {
            var info = LevelDict.Instance.GetLevelInfo(i);
            if (info != null) infos.levelList.Add(info);
        }

        var jsonInfo = JsonUtility.ToJson(infos);
        AndroidUtil.Log(jsonInfo);

        if (isNetwork)
            SaveToNetwork(jsonInfo);
        else
            SaveToLocal(jsonInfo);
    }

    private void SaveToNetwork(string jsonInfo)
    {
        //TODO 发送post请求
    }

    private void SaveToLocal(string jsonInfo)
    {
        //放在Resources文件夹的内容是只读的，无法修改
    }

    public void GetUserInfo(ICallBack callBack)
    {
        GetByNetWork(DoMain + "api/get/user_data", callBack);
    }
}