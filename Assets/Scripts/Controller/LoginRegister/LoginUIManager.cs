using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    public GameObject registerPanelGo;

    public void OnClickShowRegisterPanleBtn()
    {
        registerPanelGo.SetActive(true);
    }

    public void OnClickCloseRegisterPanelBtn()
    {
        registerPanelGo.SetActive(false);
    }
}