using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModRecScript : MonoBehaviour
{
    [TextArea] public string results;
    public HWCHandler hwcHandler;

    public void TestHWRRec()
    {
       //TODO 取消注释
        hwcHandler.TestHWRRec(results);
        results = "";
    }
}