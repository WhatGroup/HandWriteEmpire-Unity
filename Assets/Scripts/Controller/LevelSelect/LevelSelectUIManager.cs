using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelSelectUIManager : MonoBehaviour
{
    public Image AttackRole;
    public Image DefenseRole;
    public Image CureRole;

    void Start()
    {
        UpdateInfo();
    }

    void UpdateInfo()
    {
        StartCoroutine(UpdatePortrait(AttackRole,
            HttpHandler.RemotePath + UserInfoManager._instance.GetAttackRolePortraitUri()));

        StartCoroutine(UpdatePortrait(CureRole,
            HttpHandler.RemotePath + UserInfoManager._instance.GetCureRolePortraitUri()));

        StartCoroutine(UpdatePortrait(DefenseRole,
            HttpHandler.RemotePath + UserInfoManager._instance.GetDefenseRolePortraitUri()));
    }

    IEnumerator UpdatePortrait(Image rolePortrait, string url)
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
                rolePortrait.sprite = sprite;
                yield return new WaitForSeconds(0.01f);
                Resources.UnloadUnusedAssets();
            }
        }
    }
}