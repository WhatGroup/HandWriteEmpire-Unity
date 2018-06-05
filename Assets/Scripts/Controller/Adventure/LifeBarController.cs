using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    [SerializeField] private float fillAmount;
    [SerializeField] private Image content;
    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform barTransform;
    public float MaxValue { get; set; }

    public float Value
    {
        set { fillAmount = Map(value, 0, MaxValue, 0, 1); }
    }

    private void Update()
    {
        HandleBar();
        Quaternion rotation = headTransform.localRotation;
        rotation.z = -rotation.z;
        barTransform.localRotation = rotation;
    }

    private void HandleBar()
    {
        if (fillAmount != content.fillAmount) content.fillAmount = fillAmount;
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}