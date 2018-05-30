using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour, HttpHandler.ICallBack
{
    public Image portrait;

    public Text attackValue;

    public Text defenseValue;

    public Text cureValue;

    public int retryNetWorkTime;

    // Use this for initialization
    void Start()
    {
        RequestUserInfo();
    }


    private void RequestUserInfo()
    {
        HttpHandler._instance.GetUserInfo(this);
    }

    public void OnRequestError(string error)
    {
        AndroidUtil.Toast("网络出错!\n" + error);
        StartCoroutine(LaterRequest(retryNetWorkTime));
    }

    private IEnumerator LaterRequest(float second)
    {
        yield return new WaitForSeconds(second);
        RequestUserInfo();
    }

    public void OnRequestSuccess(string response)
    {
        var userInfo = JsonUtility.FromJson<UserInfo>(response);
        UserInfoManager._instance.SetUserInfo(userInfo);
        attackValue.text = userInfo.attackProperty + "";
        defenseValue.text = userInfo.defenseProperty + "";
        cureValue.text = userInfo.cureProperty + "";
        StartCoroutine(UpdatePortrait(userInfo.portrait));
//        portrait.sprite = ;
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