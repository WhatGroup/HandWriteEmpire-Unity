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
        int level = LevelDict.Instance.SelectLevel;
        int score = level * 20 - deathRoleCount * 5 - errorWordList.Count * 3 - beHurtCount;
        if (score >= 5)
            return score;
        else
            return 5;
    }

    public int RewordDefenseValue()
    {
        int level = LevelDict.Instance.SelectLevel;
        int score = level * 20 - deathRoleCount * 10 - errorWordList.Count * 3 - beHurtCount;
        if (score >= 5)
            return score;
        else
            return 5;
    }

    public int RewordCureValue()
    {
        int level = LevelDict.Instance.SelectLevel;
        int score = level * 20 - errorWordList.Count * 3 - beHurtCount;
        if (score >= 5)
            return score;
        else
            return 5;
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