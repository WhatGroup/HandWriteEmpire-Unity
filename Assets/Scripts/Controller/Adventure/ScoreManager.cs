using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

[Serializable]
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager _instance;

    public List<WordInfo> errorWordList = new List<WordInfo>();
    private int deathRoleCount = 0;
    private int defeatEnemyCount = 0;
    private int beHurtCount = 0;


    private void Awake()
    {
        _instance = this;
    }

    public void AddErrorWordList(WordInfo wordInfo)
    {
        errorWordList.Add(wordInfo);
    }

    public void AddDeathRoleCount()
    {
        deathRoleCount++;
    }

    public void AddDefeatEnemyCount()
    {
        defeatEnemyCount++;
    }

    public void AddBeHurtCount()
    {
        beHurtCount++;
    }

    public bool IsLessErrorWord(int count)
    {
        if (errorWordList.Count > count)
            return false;
        else
            return true;
    }

    public bool IsAllRoleLife()
    {
        if (deathRoleCount > 0)
            return false;
        else
            return true;
    }

    public bool IsDefeatAllEnemy()
    {
        if (defeatEnemyCount == 0)
        {
            return false;
        }

        return true;
    }

    public bool IsGameSuccess()
    {
        if (!RoleLifeManager._instance.IsAttachRoleAlive() && !RoleLifeManager._instance.IsDefenseRoleAlive())
        {
            return false;
        }

        int count = GetRewardFlagNum();
        if (count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public int RewordAttackValue()
    {
        return 10;
    }

    public int RewordDefenseValue()
    {
        return 15;
    }

    public int RewordCureValue()
    {
        return 20;
    }

    public int GetRewardFlagNum()
    {
        int count = 0;
        if (IsDefeatAllEnemy())
            count++;
        if (IsAllRoleLife())
            count++;
        if (IsLessErrorWord(4))
            count++;
        return count;
    }
}