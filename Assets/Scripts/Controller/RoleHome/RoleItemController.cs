using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleItemController : MonoBehaviour
{
    private RoleInfo roleInfo;
    public Image portrait;
    public GameObject roleInfoPanel;
    private int id;

    public int ID
    {
        get { return id; }
        set
        {
            id = value;
            roleInfo = UserInfoManager._instance.GetRoleInfoById(id);
            HttpUtil.ReplaceImageByNet(this, portrait, roleInfo.rolePortraitPath);
        }
    }

    public void OnClickItem()
    {
        if (roleInfo == null || roleInfo.state.ToInt() == 0)
            AndroidUtil.Toast("角色未解锁");
        else
            ShowRoleItemInfo(roleInfo);
    }

    private void ShowRoleItemInfo(RoleInfo roleInfo)
    {
        roleInfoPanel.SetActive(true);
        var roleItemInfoManager = roleInfoPanel.GetComponent<RoleItemInfoManager>();
        roleItemInfoManager.roleLiHui.sprite = RoleItemInfoManager._instance.unknownLiHui;
        HttpUtil.ReplaceImageByNet(this, roleItemInfoManager.roleLiHui, roleInfo.roleLiHuiPath);
        roleItemInfoManager.hpValue.text = roleInfo.roleHp;
        roleItemInfoManager.roleName.text = roleInfo.roleName;
        roleItemInfoManager.roleIntro.text = roleInfo.roleIntro;
        roleItemInfoManager.roleSkillDesc.text = roleInfo.roleSkillDesc;
        switch (roleInfo.roleType)
        {
            case RoleInfo.ATTACK:
                roleItemInfoManager.roleTypeSeal.sprite = RoleItemInfoManager._instance.attackSeal;
                break;
            case RoleInfo.CURE:
                roleItemInfoManager.roleTypeSeal.sprite = RoleItemInfoManager._instance.cureSeal;
                break;
            case RoleInfo.DEFENSE:
                roleItemInfoManager.roleTypeSeal.sprite = RoleItemInfoManager._instance.defenseSeal;
                break;
        }
    }
}