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
    public string attackValue;
    public string defenseValue;
    public string cureValue;
    public List<RoleInfo> roleInfos;
    public string userLevelInfosPath;
    public string userErrorWordInfosPath;
}