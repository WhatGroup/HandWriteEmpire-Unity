using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    public Image image;
    public Sprite normal;
    public Sprite active;
    public Sprite select;

    public void setNormal()
    {
        image.sprite = normal;
    }

    public void setActive()
    {
        image.sprite = active;
    }

    public void setSelect()
    {
        image.sprite = select;
    }
}