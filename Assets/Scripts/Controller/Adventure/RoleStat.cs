using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoleStat
{
    [SerializeField]
    private LifeBarController bar;
    [SerializeField]
    private float maxVal;
    [SerializeField]
    private float currentVal;

    public float MaxVal
    {
        get { return maxVal; }
        set
        {
            maxVal = value;
            bar.MaxValue = maxVal;
        }
    }

    public float CurrentVal
    {
        get { return currentVal; }
        set
        {
            currentVal = value;
            bar.Value = currentVal;
        }
    }
}