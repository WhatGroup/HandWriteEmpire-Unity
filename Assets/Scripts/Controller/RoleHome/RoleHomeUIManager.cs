using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RoleHomeUIManager : MonoBehaviour
{
    public Text attackValue;

    public Text defenseValue;

    public Text cureValue;

    public GameObject roleInfoPanel;

    [HideInInspector] public static RoleHomeUIManager _instance;

    [HideInInspector] public string currentDragRole;


    private void Awake()
    {
        _instance = this;
        if (UserInfoManager._instance.GetUserInfo() == null) BackHandler._instance.GoToMain();
    }

    // Use this for initialization
    private void Start()
    {
        if (UserInfoManager._instance.GetUserInfo() != null) UpdateUserInfo();
    }

    private void UpdateUserInfo()
    {
        var userInfo = UserInfoManager._instance.GetUserInfo();
        attackValue.text = userInfo.attackValue + "";
        defenseValue.text = userInfo.defenseValue + "";
        cureValue.text = userInfo.cureValue + "";
    }

    public void OnClickCloseRoleInfoPanel()
    {
        roleInfoPanel.SetActive(false);
    }
}