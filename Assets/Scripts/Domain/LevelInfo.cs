using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelInfo
{
    public string state;
    public int flag; //0表示锁定或者当前关卡；1,2,3分别表示旗的个数
    public string dataUrl;
    public int level;
}