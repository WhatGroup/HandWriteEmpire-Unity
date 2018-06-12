using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelSelectUIManager : MonoBehaviour
{
    public static LevelSelectUIManager _instance;

    public UnityArmatureComponent beginLoadingAnim;

    public Image attackRolePortrait;
    public Image defenseRolePortrait;
    public Image cureRolePortrait;

    public Text attackRoleName;
    public Text defenseRoleName;
    public Text cureRoleName;

    void Awake()
    {
        _instance = this;
        if (UserInfoManager._instance.GetUserInfo() == null)
        {
            BackHandler._instance.GoToMain();
        }
    }

    void Start()
    {
        UpdateInfo();
//        beginLoadingAnim.AddDBEventListener();
    }

    void UpdateInfo()
    {
        HttpUtil.ReplaceImageByNet(this, attackRolePortrait,
            UserInfoManager._instance.GetAttackRolePortraitUri());

        HttpUtil.ReplaceImageByNet(this, cureRolePortrait,
            UserInfoManager._instance.GetCureRolePortraitUri());

        HttpUtil.ReplaceImageByNet(this, defenseRolePortrait,
            UserInfoManager._instance.GetDefenseRolePortraitUri());

        attackRoleName.text = UserInfoManager._instance.GetAttackRoleName();
        defenseRoleName.text = UserInfoManager._instance.GetDefenseRoleName();
        cureRoleName.text = UserInfoManager._instance.GetCureRoleName();
    }
}