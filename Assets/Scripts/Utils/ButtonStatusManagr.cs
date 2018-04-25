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
    public float waitTime;

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
        btnIconDefensen.setSelect();
    }

    public void selectAttach()
    {
        btnIconAttach.setSelect();
    }

    public void selectCure()
    {
        btnIconCure.setSelect();
    }

    public void recoverNormal()
    {
        setAllNormal();
    }
}