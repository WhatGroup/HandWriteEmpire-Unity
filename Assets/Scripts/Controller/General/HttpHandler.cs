using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

public class HttpHandler : MonoBehaviour
{
    private const string DoMain = "http://139.199.88.206/";

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
        _instance = this;
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
            GetByNetWork(GetInfosURL + "levelInfos.json", callBack);
        else
            GetByLocal(GetJsonFilesURL + "levelInfos", callBack);

        AndroidUtil.Log(isNetwork ? "网络" : "本地" + "加载\n文件名: " + "levelInfo.json");
    }

    //TODO 请求数据处理,后续考虑是网络加载还是本地加载
    public void GetWordInfo(ICallBack callBack)
    {
        string jsonFileName = "";

        if (URLTransfer._instance.url.Equals(""))
        {
            //随机请求一个文件
            var fileName = new Random().Next(maxFileValue - minFileValue + 1) + minFileValue;
            jsonFileName = fileName + "";
        }

        jsonFileName = URLTransfer._instance.url;

        if (isNetwork)
            GetByNetWork(GetInfosURL + jsonFileName + ".json", callBack);
        else
            GetByLocal(GetJsonFilesURL + jsonFileName, callBack);

        AndroidUtil.Log(isNetwork ? "网络" : "本地" + "加载\n文件名: " + jsonFileName + ".json");
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
        callBack.OnRequestSuccess(ta.text);
    }
}