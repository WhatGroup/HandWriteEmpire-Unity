using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class LevelDict
{
    private Dictionary<int, LevelInfo> levelDict;

    private static LevelDict _instance;

    //设置选择的关卡，用于在冒险模式中判断选择了哪一个关卡
    public int SelectLevel { get; set; }

    public static LevelDict Instance
    {
        get
        {
            if (_instance == null) _instance = new LevelDict();
            return _instance;
        }
    }

    private LevelDict()
    {
        SelectLevel = 0;
        levelDict = new Dictionary<int, LevelInfo>();
    }

    //添加关卡数据
    public void AddLevelInfo(LevelInfo levelInfo)
    {
        levelDict.Add(levelInfo.level, levelInfo);
    }

    //获取关卡数据
    public LevelInfo GetLevelInfo(int level)
    {
        return levelDict.TryGet(level);
    }


    public bool IsEmpty()
    {
        if (levelDict.Count > 0) return false;
        else return true;
    }

    public int GetCount()
    {
        return levelDict.Count;
    }

    //通过关卡之后的处理
    public void UnlockLevel(int level)
    {
        LevelInfo info = levelDict.TryGet(level);
        if (info != null)
        {
            info.state = LevelInfo.CURRENT;
        }
    }
}