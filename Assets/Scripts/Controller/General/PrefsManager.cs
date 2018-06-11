using System.Collections;
using System.Collections.Generic;
using NUnit.Compatibility;
using UnityEngine;

public class PrefsManager
{
    public static float Volume
    {
        get { return PlayerPrefs.GetFloat("volume", 0.3f); }
        set { PlayerPrefs.SetFloat("volume", value); }
    }

    public static string SilderState
    {
        get { return PlayerPrefs.GetString("slider_state", "true"); }
        set { PlayerPrefs.SetString("slider_state", value); }
    }

    public static string Token
    {
        get { return PlayerPrefs.GetString("token", ""); }
        set { PlayerPrefs.SetString("token", value); }
    }
}