using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistFun
{
    public static String printArray(string[] arr)
    {
        String result = "[ ";
        for (int i = 0; i < arr.Length; i++)
        {
            if (i == arr.Length - 1)
            {
                result += arr[i];
            }
            else
            {
                result += arr[i] + ", ";
            }
        }

        result += " ]";


        return result;
    }
}