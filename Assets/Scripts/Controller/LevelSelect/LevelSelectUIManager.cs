using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelSelectUIManager : MonoBehaviour
{
    public Image attackRolePortrait;
    public Image defenseRolePortrait;
    public Image cureRolePortrait;

    public Text attackRoleName;
    public Text defenseRoleName;
    public Text cureRoleName;

    void Start()
    {
        UpdateInfo();
    }

    void UpdateInfo()
    {
        HttpUtil.ReplaceImageByNet(this, attackRolePortrait,
            HttpUtil.RemotePath + UserInfoManager._instance.GetAttackRolePortraitUri());

        HttpUtil.ReplaceImageByNet(this, cureRolePortrait,
            HttpUtil.RemotePath + UserInfoManager._instance.GetCureRolePortraitUri());

        HttpUtil.ReplaceImageByNet(this, defenseRolePortrait,
            HttpUtil.RemotePath + UserInfoManager._instance.GetDefenseRolePortraitUri());

        attackRoleName.text = UserInfoManager._instance.GetAttackRoleName();
        defenseRoleName.text = UserInfoManager._instance.GetDefenseRoleName();
        cureRoleName.text = UserInfoManager._instance.GetCureRoleName();
    }
}