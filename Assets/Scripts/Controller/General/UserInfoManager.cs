using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;
using UnityEngine.Experimental.Rendering;

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
    //注销时调用
    public void ClearUserInfo()
    {
        userInfo = null;
        attackRole = null;
        defenseRole = null;
        cureRole = null;
    }


    public void SetUserInfo(UserInfo userInfo)
    {
        this.userInfo = userInfo;
        foreach (var roleInfo in userInfo.roleInfos)
        {
            if (roleInfo.state.ToInt() == 2)
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

    public int GetAttachRoleHp()
    {
        return attackRole.roleHp.ToInt();
    }

    public int GetDefenseRoleHp()
    {
        return defenseRole.roleHp.ToInt();
    }

    public int GetCureRoleHp()
    {
        return cureRole.roleHp.ToInt();
    }

    public int GetAttackRoleSkillValue()
    {
        return attackRole.roleSkillValue.ToInt();
    }

    public int GetDefenseRoleSkillValue()
    {
        return defenseRole.roleSkillValue.ToInt();
    }

    public int GetCureRoleSkillValue()
    {
        return cureRole.roleSkillValue.ToInt();
    }

    public int AttackValue
    {
        get { return userInfo.attackValue.ToInt(); }
        set { userInfo.attackValue = (userInfo.attackValue.ToInt() + value) + ""; }
    }

    public int DefenseValue
    {
        get { return userInfo.defenseValue.ToInt(); }
        set { userInfo.defenseValue = (userInfo.defenseValue.ToInt() + value) + ""; }
    }

    public int CureValue
    {
        get { return userInfo.cureValue.ToInt(); }
        set { userInfo.cureValue = (userInfo.cureValue.ToInt() + value) + ""; }
    }
}