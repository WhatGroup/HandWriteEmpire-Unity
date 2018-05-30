using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UserInfoManager
{
    public static UserInfoManager _instance = new UserInfoManager();
    private UserInfo userInfo;

    private UserInfoManager()
    {
    }


    public void SetUserInfo(UserInfo userInfo)
    {
        this.userInfo = userInfo;
    }

    public string GetLevelInfosUri()
    {
        if (userInfo != null)
            return userInfo.levelInfosUri;
        else
            return "res/LevelInfos/levelInfos_20180526222610.json";
    }
}