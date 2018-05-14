using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WordInfo
{
    public string pinyin;
    public string content;
    public string detail;

    public WordInfo(string pinyin, string content, string detail)
    {
        this.pinyin = pinyin;
        this.content = content;
        this.detail = detail;
    }

    public string Pinyin
    {
        get { return pinyin; }
        set { pinyin = value; }
    }

    public string Content
    {
        get { return content; }
        set { content = value; }
    }

    public string Detail
    {
        get { return detail; }
        set { detail = value; }
    }

    public int getLength()
    {
        return content.Length;
    }
}