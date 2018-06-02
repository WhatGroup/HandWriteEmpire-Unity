using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserInfo
{
    public string account;
    public string userName;
    public string portraitPath;
    public int attackValue;
    public int defenseValue;
    public int cureValue;
    public List<RoleInfo> roleInfos;
    public string userLevelInfosPath;
 }