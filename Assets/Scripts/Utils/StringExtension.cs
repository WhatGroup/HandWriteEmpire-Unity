using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtension
{

    public static int ToInt(this String str)
    {
        return Int32.Parse(str);
    }
}