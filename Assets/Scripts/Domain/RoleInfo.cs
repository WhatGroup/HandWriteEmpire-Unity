using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoleInfo
{
    public const string ATTACK = "attack";
    public const string DEFENSE = "defense";
    public const string CURE = "cure";

    public string id;
    public string state;
    public string roleName;
    public string rolePortraitPath;
    public string roleLiHuiPath;
    public string roleType;
    public string roleIntro;
    public string roleSkillDesc;
    public string unlockValue;
    public string roleHp;
    public string roleSkillValue;
}