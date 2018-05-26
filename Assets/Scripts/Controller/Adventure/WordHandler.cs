using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class WordHandler : MonoBehaviour, HttpHandler.ICallBack
{
    public static WordHandler _instance;

    [HideInInspector] public WordInfo[] infos;

    private int currentWord = 0;
    private int currentCharacter = 0;

    //输入框
    public CharacterGrid[] characterGrids;
    private int gridNums;

    //当前拼音
    public Text currentPinYin;

    //提示信息
    public GameObject tipPanel;
    public Text tipPinYin;
    public Text tipDetail;

    private string[] pinYinArray;
    private string[] characterArray;


    public ButtonStatusManager btnManager;

    private bool isCharacterFull = false;
    private bool isBtnActivite = false;


    //网络连接失败时，重新请求网络的时间
    public float retryNetWorkTime = 4f;


    private void Awake()
    {
        //如果HttpHandler没有加载过，即直接从冒险进入的话，则销毁该场景，跳到关卡选择模式
//        if (HttpHandler._instance == null)
//        {
//            BackHandler._instance.PopScene();
//            BackHandler._instance.AddScene(SceneManager.GetActiveScene().name);
//            SceneManager.LoadScene("06_LevelSelect");
//            return;
//        }
        _instance = this;
    }


    // Use this for initialization
    private void Start()
    {
        gridNums = characterGrids.Length;
        ClearText();
        RequestInfo();
    }

    private void Update()
    {
        if (!isBtnActivite)
            for (var i = 0; i < gridNums; i++)
            {
                if ("".Equals(characterGrids[i].content.text)) break;

                if (i == gridNums - 1) isCharacterFull = true;
            }

        if (isCharacterFull)
        {
            btnManager.SetAllInteractable();
            isBtnActivite = true;
            isCharacterFull = false;
        }
    }

    private void ClearText()
    {
        for (var i = 0; i < gridNums; i++)
        {
            characterGrids[i].pinyin.text = "";
            characterGrids[i].content.text = "";
            characterGrids[i].toggle.interactable = false;
        }

        currentPinYin.text = "";
        tipPinYin.text = "";
        tipDetail.text = "";
    }

    public void SetBtnStateValue(bool state)
    {
        isBtnActivite = state;
    }

    //请求网络数据
    private void RequestInfo()
    {
        HttpHandler._instance.GetWordInfo(this);
    }

    //网络请求失败回调,如果请求失败则反复请求网络，直到成功
    public void OnRequestError(string error)
    {
        AndroidUtil.Toast("网络出错!\n" + error);
        StartCoroutine(LaterRequest(retryNetWorkTime));
    }

    private IEnumerator LaterRequest(float second)
    {
        yield return new WaitForSeconds(second);
        RequestInfo();
    }

    //网络请求成功回调
    public void OnRequestSuccess(string response)
    {
        AdventureHandler._instance.isCalcTime = true;
        var infoArray = JsonUtility.FromJson<WordInfoArray>(GeneralUtils.JsonArrayToObject(response, "infos"));
        infos = infoArray.infos;
        for (var i = 0; i < gridNums; i++) characterGrids[i].toggle.interactable = true;

        //更新输入框中的信息
        UpdateWordInfo(infos[currentWord]);
        //显示手写识别模块
        GameSetting._instance.SetHWRModule(true);
        //TODO 提示信息，需要删除
        AndroidUtil.Toast("该组长度:" + infos.Length);
    }

    public void UpdateWordInfo(WordInfo Word)
    {
        currentCharacter = 0;
        pinYinArray = Word.Pinyin.Split(' ');
        characterArray = Word.Content.Split(' ');
        AndroidUtil.Log("拼音: " + GeneralUtils.printArray(pinYinArray));
        AndroidUtil.Log("内容: " + GeneralUtils.printArray(characterArray));
        for (var i = 0; i < pinYinArray.Length; i++)
        {
            characterGrids[i].pinyin.text = pinYinArray[i];
            characterGrids[i].content.text = "";
        }

        currentPinYin.text = pinYinArray[0];
        characterGrids[0].toggle.isOn = true;

        tipPinYin.text = infos[currentWord].Pinyin;
        tipDetail.text = "释义:" + infos[currentWord].Detail;
    }

    public void UpdateWordInfo()
    {
        currentWord++;
        UpdateWordInfo(infos[currentWord]);
    }


    public void SetCharacter(string character)
    {
        characterGrids[currentCharacter].content.text = character;
        if (currentCharacter < gridNums - 1)
            currentCharacter++;
        else
            currentCharacter = 0;

        characterGrids[currentCharacter].toggle.isOn = true;
        currentPinYin.text = pinYinArray[currentCharacter];
    }

    public bool JudgeResult()
    {
        for (var i = 0; i < gridNums; i++)
        {
            AndroidUtil.Log("对比:" + characterArray[i] + "," + characterGrids[i].content.text);
            if (!characterArray[i].Equals(characterGrids[i].content.text))
            {
                //TODO 提示信息
                AndroidUtil.Toast("正确书写为:" + infos[currentWord].Content);
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
            //修改并保存用户数据
            int selectLevel = LevelDict.Instance.SelectLevel;
            if (selectLevel != 0)
            {
                var info = LevelDict.Instance.GetLevelInfo(selectLevel);
                info.flag = new Random().Next(2) + 1; //TODO 目前为随机生成,需要个计算公式
                if (info.state == LevelState.CURRENT)
                {
                    info.state = LevelState.OK;
                    LevelDict.Instance.UnlockLevel(selectLevel + 1);
                }

                //TODO 保存数据
                HttpHandler._instance.SaveLevelInfo();
            }

            ClearText();
            return true;
        }

        return false;
    }

    public void ShowDetialContent()
    {
        tipPanel.SetActive(true);
    }

    public void HideDetialContent()
    {
        tipPanel.SetActive(false);
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