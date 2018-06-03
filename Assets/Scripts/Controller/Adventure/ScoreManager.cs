using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[Serializable]
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager _instance;

    private List<WordInfo> errorWordList = new List<WordInfo>();
    private int deathRoleCount;
    private int defeatEnemyCount;
    private int beHurtCount;

    private bool isSuccess;

    public bool IsSuccess
    {
        get { return isSuccess; }
        set { isSuccess = value; }
    }


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
        return true;
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