using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserLevelInfoList
{
    public List<LevelInfo> userLevelInfos;

    public UserLevelInfoList(List<LevelInfo> levelInfos)
    {
        this.userLevelInfos = levelInfos;
    }
}