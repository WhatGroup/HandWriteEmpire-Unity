using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChineseInfo
{
    private string pinyin;
    private string content;
    private string tip;

    public ChineseInfo(string pinyin, string content, string tip)
    {
        this.pinyin = pinyin;
        this.content = content;
        this.tip = tip;
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

    public string Tip
    {
        get { return tip; }
        set { tip = value; }
    }
    public int getLength()
    {
        return content.Length;
    }
}