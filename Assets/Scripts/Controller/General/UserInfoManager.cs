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

    public UserInfo GetUserInfo()
    {
        return userInfo;
    }

    public string GetLevelInfosUri()
    {
        if (userInfo != null)
            return userInfo.userLevelInfosPath;
        else
            return "res/userLevelInfos/userlevelInfos_20180526222610.json";
    }

    public string GetAttackRolePortraitUri()
    {
        foreach (var roleInfo in userInfo.roleInfos)
        {
            if (roleInfo.state == 2 && roleInfo.roleType == RoleInfo.ATTACK)
            {
                return roleInfo.rolePortraitPath;
            }
        }

        return "res/images/rolePortrait/role_20180526221915.jpg";
    }

    public string GetDefenseRolePortraitUri()
    {
        foreach (var roleInfo in userInfo.roleInfos)
        {
            if (roleInfo.state == 2 && roleInfo.roleType == RoleInfo.DEFENSE)
            {
                return roleInfo.rolePortraitPath;
            }
        }

        return "res/images/rolePortrait/role_20180526221915.jpg";
    }

    public string GetCureRolePortraitUri()
    {
        foreach (var roleInfo in userInfo.roleInfos)
        {
            if (roleInfo.state == 2 && roleInfo.roleType == RoleInfo.CURE)
            {
                return roleInfo.rolePortraitPath;
            }
        }

        return "res/images/rolePortrait/role_20180526221915.jpg";
    }
}