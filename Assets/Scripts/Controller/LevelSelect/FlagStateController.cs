using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagStateController : MonoBehaviour
{
    public Sprite flagOne;
    public Sprite flagTwo;
    public Sprite flagThree;


    public Image flagImage;
    

    public void SetFlagIcon(int count)
    {
        switch (count)
        {
            case 1:
                flagImage.sprite = flagOne;
                break;
            case 2:
                flagImage.sprite = flagTwo;
                break;
            case 3:
                flagImage.sprite = flagThree;
                break;
            default:
                flagImage.sprite = flagOne;
                break;
        }
    }
}