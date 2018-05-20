using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelListHandler : MonoBehaviour, HttpHandler.ICallBack
{
    public GameObject LevelItemPrefab;

    public GameObject LevelListGo;
    private GridLayoutGroup levelGridGroup;

    private List<LevelInfo> levelList;

    private Dictionary<int, LevelInfo> levelDict;

    //网络连接失败时，重新请求网络的时间
    public float retryNetWorkTime = 4f;

    private void Start()
    {
        levelGridGroup = LevelListGo.GetComponent<GridLayoutGroup>();
        RequestLevelInfos();
    }

    //获取关卡的数据
    private void RequestLevelInfos()
    {
        HttpHandler._instance.GetLevelInfos(this);
    }


    public void OnRequestError(string error)
    {
        AndroidUtil.Toast("网络出错!\n" + error);
        StartCoroutine(LaterRequest(retryNetWorkTime));
    }

    private IEnumerator LaterRequest(float second)
    {
        yield return new WaitForSeconds(second);
        RequestLevelInfos();
    }

    public void OnRequestSuccess(string response)
    {
        //加载关卡列表
        var levelInfos = JsonUtility.FromJson<LevelInfos>(response);
        levelList = levelInfos.levelList;
        if (levelDict == null)
            levelDict = new Dictionary<int, LevelInfo>();
        foreach (var levelInfo in levelList) levelDict.Add(levelInfo.level, levelInfo);

        initLevelListItem();
    }

    private void initLevelListItem()
    {
        for (var i = 1; i <= levelDict.Count; i++)
        {
            var levelItem = Instantiate(LevelItemPrefab);
            levelItem.transform.SetParent(LevelListGo.transform, false);
            var itemController = levelItem.GetComponent<ItemStateController>();
            var flagController = levelItem.GetComponent<FlagStateController>();
            var clickHandler = levelItem.GetComponent<ClickHandler>();
            //控制图标的位置，偶数在左，奇数在右
            if (i % 2 == 0)
                SetItemContentLeft(itemController);
            else
                SetItemContentRight(itemController);

            var levelInfo = levelDict.TryGet(i);
            if (levelInfo.state.Equals(LevelState.CURRENT))
            {
                //TODO 定位到当前的位置
                itemController.CurrenLevel.SetActive(true);
            }
            else if (levelInfo.state.Equals(LevelState.OK))
            {
                itemController.OkLevel.SetActive(true);
                flagController.SetFlagIcon(levelInfo.flag);
            }
            else if (levelInfo.state.Equals(LevelState.LOCK))
            {
                itemController.LockLevel.SetActive(true);
            }

            clickHandler.levelWordInfoUrl = levelInfo.data;
        }
    }


    //设置图标在左
    private void SetItemContentRight(ItemStateController controller)
    {
        SetPanelPositionX(controller.CurrenLevel.transform, 180);
        SetPanelPositionX(controller.OkLevel.transform, 180);
        SetPanelPositionX(controller.LockLevel.transform, 180);
    }


    //设置关卡图标在右
    private void SetItemContentLeft(ItemStateController controller)
    {
        SetPanelPositionX(controller.CurrenLevel.transform, -180);
        SetPanelPositionX(controller.OkLevel.transform, -180);
        SetPanelPositionX(controller.LockLevel.transform, -180);
    }

    private void SetPanelPositionX(Transform transform, int positionX)
    {
        var temp = transform.localPosition;
        temp.x = positionX;
        transform.localPosition = temp;
    }
}