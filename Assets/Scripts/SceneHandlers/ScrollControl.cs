﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollControl : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public float smoothSpeed = 10;

    private ScrollRect scrollLevel;

    private float[] pageIndex = new float[] {0, 0.2093f, 0.5233f, 1};

    private float destionPos = 0;

    private bool isDraging = false;
//    private float CurrentPos;

    private void Start()
    {
        scrollLevel = GetComponent<ScrollRect>();
    }

    private void Update()
    {
        if (!isDraging)
            scrollLevel.horizontalNormalizedPosition =
                Mathf.Lerp(scrollLevel.horizontalNormalizedPosition, destionPos, Time.deltaTime * smoothSpeed);
        if (scrollLevel.horizontalNormalizedPosition < 0)
        {
            scrollLevel.horizontalNormalizedPosition = 0;
        }
        else if (scrollLevel.horizontalNormalizedPosition > 1)
        {
            scrollLevel.horizontalNormalizedPosition = 1;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraging = true;
    }

    //定义拖拽结束的方法，只要有ScrollRect就会自动调用，并不需要手动添加
    public void OnEndDrag(PointerEventData eventData)
    {
//        print(scrollLevel.normalizedPosition);
        var endPosX = scrollLevel.horizontalNormalizedPosition;
        var minDis = Math.Abs(pageIndex[0] - endPosX);
        var index = 0;
        for (var i = 1; i < pageIndex.Length; i++)
        {
            var temp = Math.Abs(pageIndex[i] - endPosX);
            if (temp < minDis)
            {
                minDis = temp;
                index = i;
            }
        }

//        scrollLevel.horizontalNormalizedPosition = pageIndex[index];
        destionPos = pageIndex[index];
        isDraging = false;
    }

    public void ChangeToPage1(bool isOn)
    {
        if (isOn) destionPos = pageIndex[0];
    }

    public void ChangeToPage2(bool isOn)
    {
        if (isOn) destionPos = pageIndex[1];
    }

    public void ChangeToPage3(bool isOn)
    {
        if (isOn) destionPos = pageIndex[2];
    }

    public void ChangeToPage4(bool isOn)
    {
        if (isOn) destionPos = pageIndex[3];
    }
}