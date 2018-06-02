using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UserInfoManager
{
    public static UserInfoManager _instance = new UserInfoManager();
    private UserInfo userInfo;

    private RoleInfo attackRole;
    private RoleInfo defenseRole;
    private RoleInfo cureRole;

    private UserInfoManager()
    {
    }


    public void SetUserInfo(UserInfo userInfo)
    {
        this.userInfo = userInfo;
        foreach (var roleInfo in userInfo.roleInfos)
        {
            if (roleInfo.state == 2)
            {
                switch (roleInfo.roleType)
                {
                    case RoleInfo.ATTACK:
                        attackRole = roleInfo;
                        break;
                    case RoleInfo.DEFENSE:
                        defenseRole = roleInfo;
                        break;
                    case RoleInfo.CURE:
                        cureRole = roleInfo;
                        break;
                }
            }
        }
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
        return attackRole.rolePortraitPath;
    }

    public string GetDefenseRolePortraitUri()
    {
        return defenseRole.rolePortraitPath;
    }

    public string GetCureRolePortraitUri()
    {
        return cureRole.rolePortraitPath;
    }

    public string GetAttackRoleName()
    {
        return attackRole.roleName;
    }

    public string GetDefenseRoleName()
    {
        return defenseRole.roleName;
    }

    public string GetCureRoleName()
    {
        return cureRole.roleName;
    }
}