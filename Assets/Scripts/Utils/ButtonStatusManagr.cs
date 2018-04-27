using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStatusManagr : MonoBehaviour
{
    public Button btnDefensen;
    public Button btnAttach;
    public Button btnCure;
    public ImageManager btnIconDefensen;
    public ImageManager btnIconAttach;
    public ImageManager btnIconCure;

    public void setAllNormal()
    {
        btnIconDefensen.setNormal();
        btnIconAttach.setNormal();
        btnIconCure.setNormal();
        btnDefensen.interactable = false;
        btnAttach.interactable = false;
        btnCure.interactable = false;
    }

    public void setAllActive()
    {
        btnIconDefensen.setActive();
        btnIconAttach.setActive();
        btnIconCure.setActive();
        btnDefensen.interactable = true;
        btnAttach.interactable = true;
        btnCure.interactable = true;
    }

    public void selectDefensen()
    {
        if (btnDefensen.interactable)
        {
            btnIconDefensen.setSelect();
        }
    }

    public void selectAttach()
    {
        if (btnAttach.interactable)
        {
            btnIconAttach.setSelect();
        }
    }

    public void selectCure()
    {
        if (btnCure.interactable)
        {
            btnIconCure.setSelect();
        }
    }

    public void recoverNormal()
    {
        if (btnDefensen.interactable)
        {
            setAllNormal();
        }
    }
}