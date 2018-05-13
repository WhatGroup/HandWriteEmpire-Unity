using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModRecScript : MonoBehaviour
{
    [TextArea] public string results;
    public AdventureHandler AdventureHandler;

    public void TestHWRRec()
    {
        AdventureHandler.TestHWRRec(results);
        results = "";
    }
}