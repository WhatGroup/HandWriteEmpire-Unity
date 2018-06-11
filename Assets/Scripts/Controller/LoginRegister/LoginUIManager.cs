using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    public Animator registerPanelAnimator;

    public void OnClickShowRegisterPanleBtn()
    {
//        registerPanelGo.SetActive(true);
        registerPanelAnimator.Play("register_panel_up");
    }

    public void OnClickCloseRegisterPanelBtn()
    {
        registerPanelAnimator.Play("register_panel_down");
    }
}