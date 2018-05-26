using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserInfo
{
    public string portrait;
    public int attachProperty;
    public int defenseProperty;
    public int cureProperty;
    public List<RoleInfo> roleInfos;
    public string levelInfosUri;
}