using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModRecScript : MonoBehaviour
{
    [TextArea] public string results;
    public SOLOHandler soloHandler;

    public void TestHWRRec()
    {
        soloHandler.TestHWRRec(results);
        results = "";
    }
}