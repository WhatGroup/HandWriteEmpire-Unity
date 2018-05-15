using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpUtil : MonoBehaviour
{
    public const string DoMain = "http://139.199.88.206/";
    public const string GetInfosURL = DoMain + "data/";
    public static HttpUtil _instance;

    void Awake()
    {
        _instance = this;
    }

    public interface ICallBack
    {
        void OnRequestError(string error);
        void OnRequestSuccess(string response);
    }

    public void Get(string url, ICallBack callBack)
    {
        StartCoroutine(GetText(url, callBack));
    }

    IEnumerator GetText(string url, ICallBack callBack)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                callBack.OnRequestError(www.error);
            }
            else
            {
                callBack.OnRequestSuccess(www.downloadHandler.text);
            }
        }
    }
}