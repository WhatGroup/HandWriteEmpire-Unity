using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStatusManager : MonoBehaviour
{
    public Button btnDefensen;
    public Button btnAttach;
    public Button btnCure;

    public void SetAllInteractable()
    {
        btnDefensen.interactable = true;
        btnAttach.interactable = true;
        btnCure.interactable = true;
    }

    public void SetAllDisableInteractable()
    {
        btnDefensen.interactable = false;
        btnAttach.interactable = false;
        btnCure.interactable = false;
    }

}