using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleLifeManager : MonoBehaviour
{
    public static RoleLifeManager _instance;

    [SerializeField] private BarStat attackRoleStat;
    [SerializeField] private BarStat defenseRoleStat;
    [SerializeField] private BarStat cureRoleStat;

    // Use this for initialization
    void Awake()
    {
        initRoleLifeData();
        _instance = this;
    }

    private void initRoleLifeData()
    {
        //测试数据
//        attackRoleStat.MaxVal = 100;
//        attackRoleStat.CurrentVal = 100;
//        defenseRoleStat.MaxVal = 100;
//        defenseRoleStat.CurrentVal = 100;
//        cureRoleStat.MaxVal = 100;
//        cureRoleStat.CurrentVal = 100;

        //实际数据
        attackRoleStat.MaxVal = UserInfoManager._instance.GetAttachRoleHp();
        attackRoleStat.CurrentVal = UserInfoManager._instance.GetAttachRoleHp();
        defenseRoleStat.MaxVal = UserInfoManager._instance.GetDefenseRoleHp();
        defenseRoleStat.CurrentVal = UserInfoManager._instance.GetDefenseRoleHp();
        cureRoleStat.MaxVal = UserInfoManager._instance.GetCureRoleHp();
        cureRoleStat.CurrentVal = UserInfoManager._instance.GetCureRoleHp();
    }

    public bool IsDefenseRoleAlive()
    {
        return defenseRoleStat.CurrentVal > 0 ? true : false;
    }

    public bool IsAttachRoleAlive()
    {
        return attackRoleStat.CurrentVal > 0 ? true : false;
    }

    public void HurtRole(string roleType, int reduceValue)
    {
        switch (roleType)
        {
            case RoleInfo.ATTACK:
                attackRoleStat.CurrentVal -= reduceValue;
                break;
            case RoleInfo.DEFENSE:
                defenseRoleStat.CurrentVal -= reduceValue;
                break;
            case RoleInfo.CURE:
                cureRoleStat.CurrentVal -= reduceValue;
                break;
        }
    }

    public void CureRole(string roleType, int increaseValue)
    {
        switch (roleType)
        {
            case RoleInfo.ATTACK:
                attackRoleStat.CurrentVal += increaseValue;
                if (attackRoleStat.CurrentVal > attackRoleStat.MaxVal)
                {
                    attackRoleStat.CurrentVal = attackRoleStat.MaxVal;
                }

                break;
            case RoleInfo.DEFENSE:
                defenseRoleStat.CurrentVal += increaseValue;
                if (defenseRoleStat.CurrentVal > defenseRoleStat.MaxVal)
                {
                    defenseRoleStat.CurrentVal = defenseRoleStat.MaxVal;
                }

                break;
            case RoleInfo.CURE:
                cureRoleStat.CurrentVal += increaseValue;
                if (cureRoleStat.CurrentVal > cureRoleStat.MaxVal)
                {
                    cureRoleStat.CurrentVal = cureRoleStat.MaxVal;
                }
                break;
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            defenseRoleStat.CurrentVal -= 10;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            defenseRoleStat.CurrentVal += 10;
        }
    }*/
}