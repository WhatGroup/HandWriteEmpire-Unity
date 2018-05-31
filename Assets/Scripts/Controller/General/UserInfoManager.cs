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
            return userInfo.levelInfosUri;
        else
            return "res/LevelInfos/levelInfos_20180526222610.json";
    }

    public string GetAttackRolePortraitUri()
    {
        foreach (var roleInfo in userInfo.roleInfos)
        {
            if (roleInfo.type == RoleType.ATTACK)
            {
                return roleInfo.rolePortrait;
            }
        }

        return "res/images/rolePortrait/role_20180526221836.jpg";
    }

    public string GetDefenseRolePortraitUri()
    {
        foreach (var roleInfo in userInfo.roleInfos)
        {
            if (roleInfo.state == 1 && roleInfo.type == RoleType.DEFENSE)
            {
                return roleInfo.rolePortrait;
            }
        }

        return "res/images/rolePortrait/role_20180526221836.jpg";
    }

    public string GetCureRolePortraitUri()
    {
        foreach (var roleInfo in userInfo.roleInfos)
        {
            if (roleInfo.type == RoleType.CURE)
            {
                return roleInfo.rolePortrait;
            }
        }

        return "res/images/rolePortrait/role_20180526221836.jpg";
    }
}