using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModRecScript : MonoBehaviour
{
    public Text editText;
    public SOLOHandler soloHandler;

    public void TestHWRRec()
    {
        soloHandler.TestHWRRec(editText.text);
        editText.text = "";
    }
}