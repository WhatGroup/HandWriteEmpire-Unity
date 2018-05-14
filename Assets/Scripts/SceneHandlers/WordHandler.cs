using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordHandler : MonoBehaviour
{
    public static WordHandler _instance;

    [HideInInspector] public WordInfo[] infos;

    private int currentWord = 0;
    private int currentCharacter = 0;

    public CharacterGrid[] characterGrids;
    private int gridNums;

    private string[] pinYinArray;
    private string[] characterArray;


    public ButtonStatusManager btnManager;

    public Text currentPinYin;

    private bool isCharacterFull = false;
    private bool isBtnActivite = false;

    private void Awake()
    {
        _instance = this;
    }


    // Use this for initialization
    private void Start()
    {
        gridNums = characterGrids.Length;
        RequestInfo();
        UpdateWordInfo(infos[currentWord]);
    }

    private void Update()
    {
        if (!isBtnActivite)
        {
            for (int i = 0; i < gridNums; i++)
            {
                if ("".Equals(characterGrids[i].content.text))
                {
                    break;
                }

                if (i == gridNums - 1)
                {
                    isCharacterFull = true;
                }
            }
        }

        if (isCharacterFull)
        {
            btnManager.SetAllInteractable();
            isBtnActivite = true;
            isCharacterFull = false;
        }
    }

    public void SetBtnStateValue(bool state)
    {
        isBtnActivite = state;
    }

    //请求网络数据

    private void RequestInfo()
    {
        infos = new WordInfo[4];
        infos[0] = new WordInfo("nǐ hǎo", "你 好", "用于有礼貌的打招呼或表示与人见面时的问候");
        infos[1] = new WordInfo("kē jì", "科 技", "社会上习惯于把科学和技术连在一起，统称为“科技”。实际二者既有密切联系，又有重要区别。科学解决理论问题，技术解决实际问题");
        infos[2] = new WordInfo("xiàn zài", "现 在", "现世,今生;眼前一刹那");
        infos[3] = new WordInfo("wèi lái", "未 来", "从现在往后的时间");
    }

    public void UpdateWordInfo(WordInfo Word)
    {
        currentCharacter = 0;
        pinYinArray = Word.Pinyin.Split(' ');
        characterArray = Word.Content.Split(' ');
        AndroidUtil.Log("拼音: " + GeneralTools.printArray(pinYinArray));
        AndroidUtil.Log("内容: " + GeneralTools.printArray(characterArray));
        for (int i = 0; i < pinYinArray.Length; i++)
        {
            characterGrids[i].pinyin.text = pinYinArray[i];
            characterGrids[i].content.text = "";
        }

        currentPinYin.text = pinYinArray[0];
        characterGrids[0].toggle.isOn = true;
    }

    public void UpdateWordInfo()
    {
        currentWord++;
        UpdateWordInfo(infos[currentWord]);
    }


    public void SetCharacter(String character)
    {
        characterGrids[currentCharacter].content.text = character;
        if (currentCharacter < gridNums - 1)
        {
            currentCharacter++;
        }
        else
        {
            currentCharacter = 0;
        }

        characterGrids[currentCharacter].toggle.isOn = true;
        currentPinYin.text = pinYinArray[currentCharacter];
    }

    public bool JudgeResult()
    {
        for (int i = 0; i < gridNums; i++)
        {
            AndroidUtil.Log("对比:" + characterArray[i] + "," + characterGrids[i].content.text);
            if (!characterArray[i].Equals(characterGrids[i].content.text))
            {
                return false;
            }
        }

        return true;
    }

    public bool JudgeGameOver()
    {
        SetBtnStateValue(false);
        if (currentWord == infos.Length - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowDetialContent()
    {
        AndroidUtil.Toast(infos[currentWord].Detail, 0, 0);
    }


    public void SetNewPinYin()
    {
    }

    public void SelecetCharacter1()
    {
        currentCharacter = 0;
        currentPinYin.text = pinYinArray[currentCharacter];
    }

    public void SelecetCharacter2()
    {
        currentCharacter = 1;
        currentPinYin.text = pinYinArray[currentCharacter];
    }
}