using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通过工具，用于打印数组等功能，后续待添加
public class GeneralUtils
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