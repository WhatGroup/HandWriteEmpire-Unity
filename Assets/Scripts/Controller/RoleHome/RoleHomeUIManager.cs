using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleHomeUIManager : MonoBehaviour
{
    public Text attackValue;

    public Text defenseValue;

    public Text cureValue;

    // Use this for initialization
    void Start()
    {
        if (UserInfoManager._instance.GetUserInfo() != null)
        {
            UpdateUserInfo();
        }
    }

    private void UpdateUserInfo()
    {
        var userInfo = UserInfoManager._instance.GetUserInfo();
        attackValue.text = userInfo.attackValue + "";
        defenseValue.text = userInfo.defenseValue + "";
        cureValue.text = userInfo.cureValue + "";
    }

    // Update is called once per frame
    void Update()
    {
    }
}