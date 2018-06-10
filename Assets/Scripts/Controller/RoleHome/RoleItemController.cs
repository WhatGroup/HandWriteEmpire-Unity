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
        {
            AndroidUtil.Toast("角色未解锁");
        }
        else
        {
            ShowRoleItemInfo(roleInfo);
        }
    }

    private void ShowRoleItemInfo(RoleInfo roleInfo)
    {
        Debug.Log(roleInfo.roleName);
    }
}