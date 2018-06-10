using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleItemInfoManager : MonoBehaviour
{
    public static RoleItemInfoManager _instance;

    public Sprite attackSeal;

    public Sprite defenseSeal;

    public Sprite cureSeal;

    public Sprite unknownLiHui;

    public Image roleLiHui;

    public Text hpValue;

    public Image roleTypeSeal;

    public Text roleIntro;

    public Text roleSkillDesc;

    public Text roleName;

    // Use this for initialization
    void Awake()
    {
        _instance = this;
    }
}